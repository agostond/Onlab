#ifndef __FM_H
#define __FM_H


#include "stm32f1xx_hal.h"
#include "stdbool.h"
#include "PasswordManager.h"
#include "stm32f1xx_hal_flash_ex.h"

#define NUMOFPAGES 4
#define MAXRECORDSNUM 32
#define EMPTY_FLASH_WORD 0xFFFFFFFF


#define FLASH_USER_DATA_ADDRESS 0x800E000
#define FLASH_START_ADDRESS_FIRST_BLOCK 0x800E400
#define FLASH_START_ADDRESS_SECOND_BLOCK 0x800F400
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
	R_NO_SUCH_RECORD,
	R_DECRYPTING_ERROR
};


int WriteDataToFlash(uint8_t* data, size_t n_bytes, uint32_t flashAddress);
int NewRecordToFlash(Record* newRecord);

int NthRecord(int n, bool onlyValid, Record* nthRecord);
int NumberOfValidRecords();
int GetMaxRecordCount();

int MakeNthInvalid(int n);
void MassErase();




#endif
