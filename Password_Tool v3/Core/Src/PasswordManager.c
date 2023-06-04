#include "main.h"
#include "Communication.h"
#include "Keyboard.h"
#include "PasswordManager.h"
#include "flash_handler.h"
#include "Write.h"


/**
  * @brief Types the selected password in
  *
  *
  * @param which: The serial number of the selected password.
  *
  * @param how: 0: Only password. 1: only username. 2: both username and password.
  *
  * @retval Succes or fail.
  */
uint8_t WritePass(uint8_t which, uint8_t how)
{

	HAL_Delay(50);

	Record RxBuffer;

	NthRecord(which, 1, &RxBuffer);

	//send an alt + tab via USB
	writeAltTab();
	HAL_Delay(50);

	switch (how)
	{
		case 0:
		{
			KeyBoardPrint(RxBuffer.password, sizeof(RxBuffer.password));
			break;
		}
		case 1:
		{
			KeyBoardPrint(RxBuffer.username, sizeof(RxBuffer.username));
			break;
		}
		case 2:
		{
			KeyBoardPrint(RxBuffer.username, sizeof(RxBuffer.username));
			//types tabs between username and password
			for(uint8_t i = 0; i < RxBuffer.tabNum; i++)
			{
				KeyBoardPrint("\t", 1);
			}
			//types enters after password
			KeyBoardPrint(RxBuffer.password, sizeof(RxBuffer.password));
			for(uint8_t i = 0; i < RxBuffer.enterNum; i++)
			{
					KeyBoardPrint("\n", 1);
			}
			break;
		}
		default:
		{
			return 0;
		}
	}

	return 1;
}

/**
  * @brief Creates a new record in flash.
  *
  *
  * @param Record: A struct, which stores every information about a record.
  *
  * @retval Succes or fail.
  */
uint8_t CreateRecord(Record* NewRecord)
{
	return NewRecordToFlash(NewRecord);
}


/**
  * @brief Edits the selected record.
  *
  * @note This function deletes the selected record, and creates a newone.
  *
  * @param Record: A struct, which stores every iunformation about a record.
  *
  * @param which: The serial number of the selected record.
  *
  * @retval Succes or fail
  */
uint8_t EditRecord(Record* NewRecord, uint8_t which)
{


	// Invalidate selected record.
	MakeNthInvalid(which);

	// We have to create a new record.
	return CreateRecord(NewRecord);


}


/**
  * @brief Returns the selected record.
  *
  * @param RxBuffer: The function writes the selected record into this pointer.
  *
  * @param n: The serial number of the selected record.
  *
  */
void GetNthRecord(Record* RxBuffer, int n)
{
	int errorCode = NthRecord(n, 1, RxBuffer);
	if(errorCode != F_SUCCESS)
	{
		failCom();
	}

}

/**
  * @brief Returns the selected records password.
  *
  * @param password: The function writes the selected password into this pointer.
  *
  * @param n: The serial number of the selected record.
  *
  */
void GetNthPassword(int n,  char* password)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	strcpy(password, RxBuffer.password);
}

/**
  * @brief Returns the selected records Username.
  *
  * @param username: The function writes the selected username into this pointer.
  *
  * @param n: The serial number of the selected record.
  *
  */
void GetNthUsername(int n,  char* username)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	strcpy(username, RxBuffer.username);
}

/**
  * @brief Returns the selected records pagename.
  *
  * @param name: The function writes the selected pagename into this pointer.
  *
  * @param n: The serial number of the selected record.
  *
  */
void GetNthName(int n, char* name)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	strcpy(name, RxBuffer.pageName);
}

/**
  * @brief Returns the selected records enter numbers (enters pressed after typing password).
  *
  * @param n: The serial number of the selected record.
  *
  * @retval The number of enters after password
  */
int GetNthEnterNum(int n)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	return RxBuffer.enterNum;
}


/**
  * @brief Returns the selected records tabulator numbers (tabs between username and password).
  *
  * @param n: The serial number of the selected record.
  *
  * @retval Tshe number of enters after password
  */
int GetNthTabNum(int n)
{
	Record RxBuffer;
	GetNthRecord(&RxBuffer, n);
	return RxBuffer.tabNum;
}


/**
  * @brief Makes the selected record invalid.
  *
  * @param n: The serial number of the selected record.
  *
  * @retval Succes or fail
  */
uint8_t DeleteRecord(uint8_t which){

	MakeNthInvalid(which);

	return 1;
}

