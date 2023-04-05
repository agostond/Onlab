#include "main.h"
#include "Communication.h"
#include "Keyboard.h"
#include "PasswordManager.h"
#include "flash_handler.h"







uint8_t WritePass(uint8_t which, uint8_t how){


	HAL_Delay(3000);


	uint32_t FlashAddress = GetValidBlockAddress();
	Record RxBuffer;

	int error_code = NthRecord(which, 1, &RxBuffer);



	switch (how){
		case 0: {
			KeyBoardPrint(RxBuffer.password, sizeof(RxBuffer.password));
			break;
		}
		case 1:{
			KeyBoardPrint(RxBuffer.username, sizeof(RxBuffer.username));
			break;
		}
		case 2: {
			KeyBoardPrint(RxBuffer.username, sizeof(RxBuffer.username));
			for(uint8_t i = 0; i < RxBuffer.tabNum; i++){
				KeyBoardPrint("\t", 1);
			}
			KeyBoardPrint(RxBuffer.password, sizeof(RxBuffer.password));
			for(uint8_t i = 0; i < RxBuffer.enterNum; i++){
					KeyBoardPrint("\n", 1);
			}
			break;
		}
		default: {
			return 0;
		}
	}

	return 1;
}


uint8_t CreateRecord(Record* NewRecord)
{
	return NewRecordToFlash(NewRecord);
}

uint8_t EditRecord(Record* NewRecord, uint8_t which){


	// invalidáljuk

	MakeNthInvalid(which);

	// új record létrehozása
	return CreateRecord(NewRecord);


}

void GetNthRecord(Record* RxBuffer, int n)
{
	uint32_t flashAddress = GetValidBlockAddress();
	NthRecord(n, 1, RxBuffer);
}

void GetNthPassword(int n,  char* password)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	strcpy(password, RxBuffer.password);
}

void GetNthUsername(int n,  char* username)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	strcpy(username, RxBuffer.username);
}

int GetNthEnterNum(int n)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	return RxBuffer.enterNum;
}

int GetNthTabNum(int n)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	return RxBuffer.tabNum;
}


void GetNthName(int n, char* name)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	strcpy(name, RxBuffer.pageName);
}



uint8_t DeleteRecord(uint8_t which){

	MakeNthInvalid(which);

	return 1;
}

