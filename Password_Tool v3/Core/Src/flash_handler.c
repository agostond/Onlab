#include "flash_handler.h"
#include "crypto.h"
#include <string.h>
#include <stdbool.h>

extern CRC_HandleTypeDef hcrc;

void MassErase()
{
	EraseInvalidBlock(FLASH_USER_DATA_ADDRESS, (1 + 2 * NUMOFPAGES));
}

int GetMaxRecordCount()
{
	return MAXRECORDSNUM;
}

int WriteDataToFlash(uint8_t* data, size_t n_bytes, uint32_t flashAddress)
{
	int sofar=0;
	int numberofwords = n_bytes/4;
	uint32_t* data_ptr = (uint32_t*)data;

	/* Unlock the Flash to enable the flash control register access *************/
	HAL_FLASH_Unlock();


	/* Program the user Flash area word by word*/

	while (sofar<numberofwords)
	{
		if (HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, flashAddress, data_ptr[sofar]) == HAL_OK)
		{
			flashAddress += 4;  // use StartPageAddress += 2 for half word and 8 for double word
		  	sofar++;
		}
		else
		{
			/* Error occurred while writing data in Flash memory*/

		  		return F_WRITING_ERROR;
		}
	}

	/* Lock the Flash to disable the flash control register access (recommended
	to protect the FLASH memory against possible unwanted operation) *********/
	HAL_FLASH_Lock();

	return F_SUCCESS;
}

int ReadRecordFromAddress(uint32_t FlashAddress, Record* RecordBuffer)
{


	if(FlashAddress < FLASH_START_ADDRESS_FIRST_BLOCK || FlashAddress >= END_OF_FLASH)
	{
		return F_INVALID_ADDRESS;
	}

	uint32_t current_flash_address = FlashAddress;



	memcpy(&RecordBuffer->isValid, (uint32_t*)current_flash_address, sizeof(RecordBuffer->isValid));
	current_flash_address += sizeof(RecordBuffer->isValid);

	memcpy(&RecordBuffer->serialNum, (uint32_t*)current_flash_address, sizeof(RecordBuffer->serialNum));
	current_flash_address += sizeof(RecordBuffer->serialNum);

	memcpy(&RecordBuffer->pageName, (uint32_t*)current_flash_address, sizeof(RecordBuffer->pageName));
	current_flash_address += sizeof(RecordBuffer->pageName);

	memcpy(&RecordBuffer->username, (uint32_t*)current_flash_address, sizeof(RecordBuffer->username));
	current_flash_address += sizeof(RecordBuffer->username);

	memcpy(&RecordBuffer->password, (uint32_t*)current_flash_address, sizeof(RecordBuffer->password));
	current_flash_address += sizeof(RecordBuffer->password);

	memcpy(&RecordBuffer->isSecure, (uint32_t*)current_flash_address, sizeof(RecordBuffer->isSecure));
	current_flash_address += sizeof(RecordBuffer->isSecure);

	memcpy(&RecordBuffer->tabNum, (uint32_t*)current_flash_address, sizeof(RecordBuffer->tabNum));
	current_flash_address += sizeof(RecordBuffer->tabNum);

	memcpy(&RecordBuffer->enterNum, (uint32_t*)current_flash_address, sizeof(RecordBuffer->enterNum));
	current_flash_address += sizeof(RecordBuffer->enterNum);

	memcpy(&RecordBuffer->sumCRC, (uint32_t*)current_flash_address, sizeof(RecordBuffer->sumCRC));

	// Kititkositas
	uint8_t keyToRecordKey[32];
	uint8_t nonce[12];
	uint8_t recordKey[32];
	memcpy(keyToRecordKey, (uint32_t*)FLASH_USER_DATA_ADDRESS ,32);
	memcpy(nonce, (uint32_t*)(FLASH_USER_DATA_ADDRESS + 20), 12);
	memcpy(recordKey, (uint32_t*)(FLASH_USER_DATA_ADDRESS + 32), 32);
	EncryptDecrypt(keyToRecordKey, nonce, recordKey, 32);

	EncryptDecrypt(recordKey, nonce, (uint8_t*)RecordBuffer->username, 32);
	EncryptDecrypt(recordKey, nonce, (uint8_t*)RecordBuffer->password, 64);

	// CRC ellenorzes
	HAL_CRC_Calculate(&hcrc, (uint32_t*)RecordBuffer->username, 8);
	uint32_t calCRC = HAL_CRC_Accumulate(&hcrc, (uint32_t*)RecordBuffer->password, 16);
	if(RecordBuffer->sumCRC != calCRC)
	{
		return R_DECRYPTING_ERROR;
	}

	return F_SUCCESS;

}



int NumberOfValidRecords()
{
	int numberOfRecords = 0;
	uint32_t buffer;
	uint32_t currentFlashAddress = GetValidBlockAddress();

	while(1)
	{
		memcpy(& buffer, (uint32_t *)currentFlashAddress, 4);
		if(buffer == EMPTY_FLASH_WORD)
		{
			return numberOfRecords;
		}

		else
		{

			if(IsValid(currentFlashAddress))
			{
				numberOfRecords ++;
			}
			currentFlashAddress += sizeof(Record);

			if(currentFlashAddress > END_OF_FLASH)
			{
				return numberOfRecords;
			}
		}
	}

}

int IsValid(uint32_t FlashAddress)
{

	uint16_t Rx;
	memcpy(&Rx, (uint16_t*)FlashAddress, 2);

	if(Rx == VALID_RECORD)
	{
		return 1;
	}

	else
	{
		return 0;
	}

}

int MakeInvalid(uint32_t FlashAddress)
{

	uint32_t address_of_isvalid = 0;

	address_of_isvalid = FlashAddress;

	HAL_FLASH_Unlock();
	if (HAL_FLASH_Program(FLASH_TYPEPROGRAM_HALFWORD, address_of_isvalid,  INVALID_RECORD) != HAL_OK)
	{
	  	return F_WRITING_ERROR;
	}

	HAL_FLASH_Lock();

	return F_SUCCESS;

}

int MakeNthInvalid(int n)
{
	uint32_t flashAddress = GetValidBlockAddress(FLASH_START_ADDRESS_FIRST_BLOCK, FLASH_START_ADDRESS_SECOND_BLOCK);
	flashAddress = AddressOfNthValidRecord(n);
	int status = MakeInvalid(flashAddress);
	return status;
}

int NthRecord(int n, bool onlyValid, Record* nthRecord)
{
	uint32_t currentFlashAddress = GetValidBlockAddress();

	if(onlyValid)
	{
		uint32_t addressOfNthValid =  AddressOfNthValidRecord(n);
		if(addressOfNthValid == R_NO_SUCH_RECORD)
		{
			return R_NO_SUCH_RECORD;
		}
		else
		{
			return ReadRecordFromAddress(addressOfNthValid, nthRecord);
		}
	}
	else
	{
		currentFlashAddress += n*(sizeof(Record));
		return ReadRecordFromAddress(currentFlashAddress, nthRecord);
	}
}

uint32_t GetFirstEmptyAddress()
{
	uint32_t FlashAddress = GetValidBlockAddress();
	int numberOfAllRecords = NumberOfAllRecords();
	uint32_t firstEmptyAddress = FlashAddress += numberOfAllRecords * (sizeof(Record));
	return firstEmptyAddress;
}

uint32_t GetValidBlockAddress()
{
	Record RxBuffer;
	int maxSerialNum;


	uint32_t addressOfLastRecordA = FLASH_START_ADDRESS_FIRST_BLOCK;
	uint32_t wordFromFlash;

	uint32_t addressOfLastRecordB = FLASH_START_ADDRESS_SECOND_BLOCK;

	int recordsInA = 0;
	int recordsInB = 0;

	while(addressOfLastRecordA != FLASH_START_ADDRESS_SECOND_BLOCK)
	{
		memcpy(&wordFromFlash, (uint32_t*)addressOfLastRecordA, sizeof(wordFromFlash));
		if(wordFromFlash == EMPTY_FLASH_WORD)
		{
			break;
		}
		else
		{
			addressOfLastRecordA += sizeof(Record);
			recordsInA++;
		}
	}

	while(addressOfLastRecordB != END_OF_FLASH)
	{
		memcpy(&wordFromFlash, (uint32_t*)addressOfLastRecordB, sizeof(wordFromFlash));
		if(wordFromFlash == EMPTY_FLASH_WORD)
		{
			break;
		}
		else
		{
			addressOfLastRecordB += sizeof(Record);
			recordsInB++;
		}
	}

	if(0 == recordsInA && 0 == recordsInB)
	{
		return FLASH_START_ADDRESS_FIRST_BLOCK;
	}
	if(0 == recordsInA)
	{
		return FLASH_START_ADDRESS_SECOND_BLOCK;
	}

	if(0 == recordsInB)
	{
		return FLASH_START_ADDRESS_FIRST_BLOCK;

	}



	int status = ReadRecordFromAddress(addressOfLastRecordA, &RxBuffer);
	/*hibakezelés*/

	if(status != F_SUCCESS)
	{
		return status;
	}

	maxSerialNum = RxBuffer.serialNum;

	status = ReadRecordFromAddress(addressOfLastRecordB, &RxBuffer);
	/*hibakezelés*/

	if(status != F_SUCCESS)
	{
		return status;
	}

	/* Annak eldöntése, hogy melyik blokkban van nagyobb sorszámú rekord, tehát melyik a valid blokk*/



	if(RxBuffer.serialNum > maxSerialNum)
	{
		return FLASH_START_ADDRESS_SECOND_BLOCK;
	}

	else
	{
		return FLASH_START_ADDRESS_FIRST_BLOCK;
	}

}

int WriteRecordToFlash(Record * recordPtr, uint32_t flashAddress)
{


	uint32_t* structPtr = (uint32_t*)recordPtr;


	int sofar=0;
	int numberofwords = sizeof(Record)/4;

	/* Unlock the Flash to enable the flash control register access *************/
	HAL_FLASH_Unlock();


	/* Program the user Flash area word by word*/

	while (sofar<numberofwords)
	{
		if (HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, flashAddress, structPtr[sofar]) == HAL_OK)
		{
			flashAddress += 4;  // use StartPageAddress += 2 for half word and 8 for double word
		  	sofar++;
		}
		else
		{
			/* Error occurred while writing data in Flash memory*/

		  		return F_WRITING_ERROR;
		}
	}

	/* Lock the Flash to disable the flash control register access (recommended
	to protect the FLASH memory against possible unwanted operation) *********/
	HAL_FLASH_Lock();

	return F_SUCCESS;
}

int EraseInvalidBlock(uint32_t FlashAddressB, uint16_t numberOfPages)
{

	//int numberofwords = (strlen((char *)Data)/4)+((strlen((char*)Data)%4)!=0);
	static FLASH_EraseInitTypeDef EraseInitStruct;
	uint32_t PAGEError;


	  /* Unlock the Flash to enable the flash control register access *************/
	   HAL_FLASH_Unlock();


	   /* Fill EraseInit structure*/
	   EraseInitStruct.TypeErase   = FLASH_TYPEERASE_PAGES;
	   EraseInitStruct.PageAddress = FlashAddressB;
	   EraseInitStruct.NbPages     = numberOfPages;

	   if (HAL_FLASHEx_Erase(&EraseInitStruct, &PAGEError) != HAL_OK)
	   {
	     /*Error occurred while page erase.*/
		  return F_ERASING_ERROR;
	   }

	HAL_FLASH_Lock();

	return F_SUCCESS;
}

int NewRecordToFlash(Record* newRecord)
{
	uint32_t flashAddress = GetValidBlockAddress();
	int nOfValidRecords = NumberOfValidRecords();
	int nOfAllRecords = NumberOfAllRecords();
	if(nOfValidRecords >= MAXRECORDSNUM)
	{
		return F_IS_FULL;
	}
	else if(nOfAllRecords >= MAXRECORDSNUM)
	{
		CleanUpFlash();
		flashAddress = GetValidBlockAddress(FLASH_START_ADDRESS_FIRST_BLOCK, FLASH_START_ADDRESS_SECOND_BLOCK);
	}

	// CRC kiszamolasa

	HAL_CRC_Calculate(&hcrc, (uint32_t*)newRecord->username, 8);
	newRecord->sumCRC = HAL_CRC_Accumulate(&hcrc, (uint32_t*)newRecord->password, 16);

	// Titkositas
	uint8_t keyToRecordKey[32];
	uint8_t nonce[12];
	uint8_t recordKey[32];
	memcpy(keyToRecordKey, (uint32_t*)FLASH_USER_DATA_ADDRESS ,32);
	memcpy(nonce, (uint32_t*)(FLASH_USER_DATA_ADDRESS + 20), 12);
	memcpy(recordKey, (uint32_t*)(FLASH_USER_DATA_ADDRESS + 32), 32);
	EncryptDecrypt(keyToRecordKey, nonce, recordKey, 32);

	EncryptDecrypt(recordKey, nonce, (uint8_t*)newRecord->username, 32);
	EncryptDecrypt(recordKey, nonce, (uint8_t*)newRecord->password, 64);

	newRecord->isSecure = 1;
	newRecord->isValid = 1;

	// sorszam beallitasa

	newRecord->serialNum = GetNextSerialNum();


	flashAddress = GetFirstEmptyAddress(flashAddress);
	return WriteRecordToFlash(newRecord, flashAddress);
}

int GetNextSerialNum()
{
	Record RxBuffer;
	int nOfAllRecords = NumberOfAllRecords();

	if(0 == nOfAllRecords)
	{
		return 0;
	}
	int errorCode = NthRecord((nOfAllRecords - 1), 1, &RxBuffer);
	return ((RxBuffer.serialNum)+1);
}

/* Ha pointerként adnám át a blokkok címét, akkor meg is cserélhetné az érvényes és nem érvényes blokkokat */
int CleanUpFlash()
{


	int status;
	uint16_t numberOfPages = NUMOFPAGES;
	uint32_t FlashAddressA;
	uint32_t FlashAddressB;

	SetFlashAddresses(&FlashAddressA, &FlashAddressB);
	EraseInvalidBlock(FlashAddressB, NUMOFPAGES);

	uint16_t numberOfRecords = NumberOfAllRecords(FlashAddressA);
	int i = 0;
	uint32_t currentFlashAddressA = FlashAddressA;
	uint32_t currentFlashAddressB = FlashAddressB;
	uint32_t tempAddress;
	Record RxBuffer;

	for(;i < numberOfRecords; i++)
	{

		if(IsValid(currentFlashAddressA))
		{
			ReadRecordFromAddress(currentFlashAddressA, &RxBuffer);
			status = WriteRecordToFlash(&RxBuffer, currentFlashAddressB);
			if(status != F_SUCCESS)
			{
				return status;
			}
			currentFlashAddressA += sizeof(Record);
			currentFlashAddressB += sizeof(Record);
		}
		else
		{
			currentFlashAddressA += sizeof(Record);
		}
	}


	tempAddress = FlashAddressA;
	FlashAddressA = FlashAddressB;
	FlashAddressB = tempAddress;



	return EraseInvalidBlock(tempAddress, numberOfPages);
}

void SetFlashAddresses(uint32_t *FlashAddressA, uint32_t *FlashAddressB)
{


	*FlashAddressA = GetValidBlockAddress();


	(*FlashAddressA == FLASH_START_ADDRESS_FIRST_BLOCK) ?
			(*FlashAddressB = FLASH_START_ADDRESS_SECOND_BLOCK) :
			(*FlashAddressB = FLASH_START_ADDRESS_FIRST_BLOCK);


}

int NumberOfAllRecords()
{
	int n = 0;

	uint32_t currentAddress = GetValidBlockAddress();
	uint32_t currentWordInFlash = 0;
	memcpy(&currentWordInFlash, (uint32_t*)currentAddress, sizeof(currentWordInFlash));
	while(currentWordInFlash != EMPTY_FLASH_WORD && n < MAXRECORDSNUM)
	{
		n++;
		currentAddress += sizeof(Record);
		memcpy(&currentWordInFlash, (uint32_t*)currentAddress, sizeof(currentWordInFlash));
	}
	return n;

}

uint32_t AddressOfNthValidRecord(int n)
{
	uint32_t address = GetValidBlockAddress();
	int numberOfRecords = NumberOfAllRecords(address);
	int i = 0;
	int nth= 0;
	for(i = 0; i<numberOfRecords; i++)
	{
		if(	IsValid(address))
		{
			if(nth == n)
			{
				return address;
			}
			nth++;
			address += sizeof(Record);
		}

		else
		{
			address += sizeof(Record);
		}

	}
	return R_NO_SUCH_RECORD;
}




