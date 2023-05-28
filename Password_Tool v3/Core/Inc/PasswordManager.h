#ifndef __PM_H
#define __PM_H


//struct for storing records
typedef struct __attribute__((packed, aligned(4)))
{
	uint16_t isValid;
	uint16_t serialNum;
	char pageName[17];
	char username [32];
	char password [64];
	uint8_t isSecure;
	uint8_t tabNum;
	uint8_t enterNum;
	uint32_t sumCRC;

} Record;


	uint8_t WritePass(uint8_t which, uint8_t how);

	void InitPass();

	uint8_t NumberOfRecords(uint32_t FlashAddress);

	void GetName(uint32_t FlashAddress, uint8_t n, char* name);

	uint8_t CreateRecord(Record* NewRecord);

	uint8_t EditRecord(Record* NewRecord, uint8_t which);

	uint8_t DeleteRecord(uint8_t which);

	void GetNthRecord(Record* RxBuffer, int n);

	void GetNthPassword(int n,  char* password);

	void GetNthUsername(int n,  char* username);

	int GetNthEnterNum(int n);

	int GetNthTabNum(int n);

	void GetNthName(int n, char* name);

#endif
