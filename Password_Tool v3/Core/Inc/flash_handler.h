#ifndef __FM_H
#define __FM_H


#include "stm32f1xx_hal.h"
#include "stdbool.h"
#include "PasswordManager.h"
#include "stm32f1xx_hal_flash_ex.h"

#define NUMOFPAGES 12
#define MAXRECORDSNUM 8
#define EMPTY_FLASH_WORD 0xFFFFFFFF

#define FLASH_USER_DATA_ADDRESS 0x800DC00
#define FLASH_START_ADDRESS_FIRST_BLOCK 0x800E000
#define FLASH_START_ADDRESS_SECOND_BLOCK 0x800F000
#define END_OF_FLASH  0x8010000


#define VALID_RECORD 1U
#define INVALID_RECORD 0U

#define INVALID_ADDRESS 0X01U

enum ErrorCodes
{
	F_SUCCESS,
	F_WRITING_ERROR,
	F_INVALID_ADDRESS,
	F_ERASING_ERROR,
	F_IS_FULL,
	R_SUCCESS,
	R_NO_SUCH_RECORD
};


void MassErase();
int GetMaxRecordCount();
int GetNextSerialNum();
int NumberOfValidRecords();
int WriteDataToFlash(uint8_t* data, size_t n_bytes, uint32_t flashAddress);

//void ReadWordsFromFlash(uint32_t FlashAddress, uint32_t* buffer, int numberofwords);

//uint32_t SearchRecordInFlash(char* key, uint32_t FlashAddress);

int ReadRecordFromAddress(uint32_t FlashAddress, Record* RecordBuffer);

int IsValid(uint32_t FlashAddress);

int MakeInvalid(uint32_t FlashAddress);
int MakeNthInvalid(int n);

int NthRecord(int n, bool onlyValid, Record* nthRecord);

uint32_t GetFirstEmptyAddress();

uint32_t GetValidBlockAddress();

int EraseInvalidBlock(uint32_t FlashAddressB, uint16_t numberOfPages);

int CleanUpFlash();

int WriteRecordToFlash(Record * recordPtr, uint32_t flashAddress);

void SetFlashAddresses(uint32_t *FlashAddressA, uint32_t *FlashAddressB);

uint32_t AddressOfNthValidRecord(int n);

int NumberOfAllRecords();

int NewRecordToFlash(Record* newRecord);



#endif
