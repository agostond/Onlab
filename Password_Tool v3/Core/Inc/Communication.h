#ifndef __COM_H
#define __COM_H

#define CREATE 0xAA
#define AUTHENTICATE 0xBB
#define VALID_REPORT 0xFF
#define FAIL 5
#define SUCCES  10
#define LOGGED_IN 0xCC
#define NOT_LOGGED_IN 0xDD

#define CHECK_SUM_PLACE 63

#define SEND_USERNAME 0
#define SEND_PASS_COUNT 1
#define SEND_PASS 2
#define SEND_PASS_NAME 3
#define ENTER_PASS 4
#define ADDING_PASS 5
#define EDIT_PASS 6
#define DEL_PASS 7
#define SEND_TAB_NUM 8
#define SEND_ENTER_NUM 9
#define SEND_MAX_PASS_COUNT 10
#define LOGOUT 11
#define SEND_STATUS 12




uint8_t SendPassCount(uint8_t checksum);

uint8_t SendName(uint8_t which, uint8_t checksum);

uint8_t SendPassword(uint8_t which, uint8_t checksum);

uint8_t SendUsername(uint8_t which, uint8_t checksum);

uint8_t SendTabNum(uint8_t which, uint8_t checksum);

uint8_t SendEnterNum(uint8_t which, uint8_t checksum);

uint8_t SendStatus(uint8_t status, uint8_t checksum);

uint8_t SendMaxPassCount(uint8_t checksum);

uint8_t SendString(char str[], uint8_t checksum);

uint8_t SendData(uint8_t* data, uint8_t length, uint8_t checksum);

uint8_t SendSingleData(uint8_t data, uint8_t checksum);

void ClearNewFeature();

void failCom();

void FeatureToPass(uint8_t type);

void HandleFeatureReport();

uint8_t AuthenticateFromFeature(char* masterPassword, uint8_t status);





#endif
