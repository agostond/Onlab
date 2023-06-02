#include "flash_handler.h"
#include "crypto.h"
#include <string.h>
#include <stdbool.h>

extern CRC_HandleTypeDef hcrc;

/**************************
 * Function Declarations
 **************************/
uint32_t GetFirstEmptyAddress();
uint32_t GetValidBlockAddress();
int NumberOfAllRecords();
void SetFlashAddresses(uint32_t *FlashAddressA, uint32_t *FlashAddressB);

int ReadRecordFromAddress(uint32_t FlashAddress, Record* RecordBuffer);

int GetNextSerialNum();
uint32_t AddressOfNthValidRecord(int n);
int IsValid(uint32_t FlashAddress);

int WriteRecordToFlash(Record * recordPtr, uint32_t flashAddress);

int MakeInvalid(uint32_t FlashAddress);
int EraseInvalidBlock(uint32_t FlashAddressB, uint16_t numberOfPages);
int CleanUpFlash();




/**************************
 * Function Definitions
 **************************/


/**
  * @brief Erase the whole flash segment of the Password Manager
  *
  * @note This function deletes all the data which the user saved on the device.
  */
void MassErase()
{
	EraseInvalidBlock(FLASH_USER_DATA_ADDRESS, (1 + 2 * NUMOFPAGES));
}

/**
  * @brief Returns the maximum possible record number the device can store
  *
  * @retval the number of records the device can store
  */
int GetMaxRecordCount()
{
	return MAXRECORDSNUM;
}

/**
  * @brief This function is used to write a byte stream to the specified flash address.
  *
  * @param data: the bytes to store
  *
  * @param n_bytes: the number of bytes to store
  *
  * @param flashAddress: the address of the flash, where the bytes need to be stored
  *
  * @retval status of the flash operation (succes or fail)
  */
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

/**
  * @brief Reads a Record from the specified flash address.
  *
  * @note 	The function decrypts the encrypted records before passing it to the buffer
  * 		The function also performs a CRC check on the decrypted Record
  *
  * @param FlashAddress: the address of the desired Record in the flash
  * @param RecordBuffer: a buffer for the Record (used as return)
  *
  *
  * @retval status of the flash operation (succes or fail)
  */
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

	// Decrypting the Record
	uint8_t keyToRecordKey[32];
	uint8_t nonce[12];
	uint8_t recordKey[32];
	memcpy(keyToRecordKey, (uint32_t*)FLASH_USER_DATA_ADDRESS ,32);
	memcpy(nonce, (uint32_t*)(FLASH_USER_DATA_ADDRESS + 20), 12);
	memcpy(recordKey, (uint32_t*)(FLASH_USER_DATA_ADDRESS + 32), 32);
	EncryptDecrypt(keyToRecordKey, nonce, recordKey, 32);

	EncryptDecrypt(recordKey, nonce, (uint8_t*)RecordBuffer->username, 32);
	EncryptDecrypt(recordKey, nonce, (uint8_t*)RecordBuffer->password, 64);

	// CRC check
	HAL_CRC_Calculate(&hcrc, (uint32_t*)RecordBuffer->username, 8);
	uint32_t calCRC = HAL_CRC_Accumulate(&hcrc, (uint32_t*)RecordBuffer->password, 16);
	if(RecordBuffer->sumCRC != calCRC)
	{
		return R_DECRYPTING_ERROR;
	}

	return F_SUCCESS;

}


/**
  * @brief Returns the number of valid records stored in the flash.
  *
  *
  * @retval the number of the valid records
  */
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

/**
  * @brief Checks weather the record is valid or not in the specified flash address.
  *
  *
  * @param FlashAddress: the address of the desired Record in the flash
  *
  *
  * @retval valid or not
  */
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

/**
  * @brief Make the Record invalid in the specified flash address.
  * @param FlashAddress: the address of the desired Record in the flash
  *
  *
  * @retval status of the flash operation (success or fail)
  */
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

/**
  * @brief Make the nth Record invalid in the flash.
  * @param n: the number of the valid record that we want to make invalid
  *
  *
  * @retval status of the flash operation (success or fail)
  */
int MakeNthInvalid(int n)
{
	uint32_t flashAddress = GetValidBlockAddress(FLASH_START_ADDRESS_FIRST_BLOCK, FLASH_START_ADDRESS_SECOND_BLOCK);
	flashAddress = AddressOfNthValidRecord(n);
	int status = MakeInvalid(flashAddress);
	return status;
}

/**
  * @brief Returns the nth Record in the specified Record buffer
  * @param n: the number of the desired record
  * @param onlyValid:	true -> we only count the valid records
  * 					false -> we count every record
  *
  * @param nthRecord: the buffer for the Record
  *
  * @retval status of the flash operation (success or fail)
  */
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

/**
  * @brief Returns the first empty address int the flash.
  *
  * @retval the address
  */
uint32_t GetFirstEmptyAddress()
{
	uint32_t FlashAddress = GetValidBlockAddress();
	int numberOfAllRecords = NumberOfAllRecords();
	uint32_t firstEmptyAddress = FlashAddress += numberOfAllRecords * (sizeof(Record));
	return firstEmptyAddress;
}

/**
  * @brief Returns the valid block address.
  *
  * @note	We use double buffering in the flash operations.
  * 		This function determines the address of the block which holds the valid data.
  *
  *
  *
  * @retval the address
  */
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


	if(status != F_SUCCESS)
	{
		return status;
	}

	if(RxBuffer.serialNum > maxSerialNum)
	{
		return FLASH_START_ADDRESS_SECOND_BLOCK;
	}

	else
	{
		return FLASH_START_ADDRESS_FIRST_BLOCK;
	}

}

/**
  * @briefWrites a Record to the specified flash address.
  *
  * @param recordPtr: the Record which need to be stored
  * @param flashAddress: the address where the Record will be stored.
  *
  *
  * @retval status of the flash operation (success or fail)
  */
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

/**
  * @brief Ereases the invalid block when cleaning the flash.
  * @param FlashAddressB:	the address of the second buffer
  *							we will store the records here
  *
  * @param numberOfPages: the number of flash pages used as buffer
  *
  * @retval status of the flash operation (success or fail)
  */
int EraseInvalidBlock(uint32_t FlashAddressB, uint16_t numberOfPages)
{

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

/**
  * @brief Saves a record to the flash
  *
  * @note	This function also performs encryption before saving the Record.
  * 		There is also a CRC operation before the saving.
  *
  * @param newRecord: the Record to save
  *
  *
  * @retval status of the flash operation (success or fail)
  */
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

	// CRC calculation

	HAL_CRC_Calculate(&hcrc, (uint32_t*)newRecord->username, 8);
	newRecord->sumCRC = HAL_CRC_Accumulate(&hcrc, (uint32_t*)newRecord->password, 16);

	// Encryption
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

	// Calculating the serial number of the record.

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
	NthRecord((nOfAllRecords - 1), 1, &RxBuffer);
	return ((RxBuffer.serialNum)+1);
}


/**
  * @brief Cleans up the flash if the number of valid Records is less then the maximum Record number, but the flash is full.
  *
  *
  * @retval status of the flash operation (success or fail)
  */
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

/**
  * @brief Sets the valid flash address.
  *
  * @param FlashAddressA: the address of the first buffer.
  * @param FlashAddressB: the address of the second buffer.
  *
  */
void SetFlashAddresses(uint32_t *FlashAddressA, uint32_t *FlashAddressB)
{


	*FlashAddressA = GetValidBlockAddress();


	(*FlashAddressA == FLASH_START_ADDRESS_FIRST_BLOCK) ?
			(*FlashAddressB = FLASH_START_ADDRESS_SECOND_BLOCK) :
			(*FlashAddressB = FLASH_START_ADDRESS_FIRST_BLOCK);


}

/**
  * @brief Returns the number of the stored records
  *
  * @note This function counts all the stored records (valid and invalid)
  *
  * @retval number of all stored records
  */
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

/**
  * @brief Calculates the address of the nth valid Record in the flash.
  * @param n: the number of the desired Record.
  *
  *
  * @retval the address of the nth Record (or error if n is out of range)
  */
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




