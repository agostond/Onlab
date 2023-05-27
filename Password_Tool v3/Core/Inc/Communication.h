#ifndef __COM_H
#define __COM_H

/***********************
 * Status IDs
 ***********************/
#define LOGGED_IN 0xCC
#define NOT_LOGGED_IN 0xDD

// User creation needed
#define CREATE 0xAA
#define AUTHENTICATE 0xBB

// Used to identify valid master password or Challenge Response
#define VALID_REPORT 0xFF

/********************************
 * Possible responses to client
 *******************************/
#define FAIL 5
#define SUCCES  10

// Used to send random number for Challenge Response authentication.
#define SEND_RND_NUM 0xAB

// Defines the index of the check sum which is used to synchronize communication
#define CHECK_SUM_PLACE 63

/***********************
 * Command IDs
 ***********************/
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
#define DELETE_ALL 13




/**
  * @brief Sends the current number of passwords stored on the device.
  *
  *
  * @param  checksum: the checksum arrived from client.
  *
  * @retval succes or fail
  */
uint8_t SendPassCount(uint8_t checksum);

/**
  * @brief Sends the name of the selected record.
  *
  *
  * @param checksum: the checksum arrived from client.
  * @param which: defines the nth record to send.
  *
  * @retval succes or fail
  */
uint8_t SendName(uint8_t which, uint8_t checksum);

/**
  * @brief Sends the password of the selected record.
  *
  *
  * @param checksum: the checksum arrived from client.
  * @param which: defines the nth record to send.
  *
  * @retval succes or fail
  */
uint8_t SendPassword(uint8_t which, uint8_t checksum);

/**
  * @brief Sends the username of the selected record.
  *
  *
  * @param checksum: the checksum arrived from client.
  * @param which: defines the nth record to send.
  *
  * @retval succes or fail
  */
uint8_t SendUsername(uint8_t which, uint8_t checksum);

/**
  * @brief Sends the number of tabs pressed between auth. credentials.
  *
  *
  * @param checksum: the checksum arrived from client.
  * @param which: defines the nth record to send.
  *
  * @retval succes or fail
  */
uint8_t SendTabNum(uint8_t which, uint8_t checksum);

/**
  * @brief Sends the number of enters after auth credentials are entered.
  *
  *
  * @param checksum: the checksum arrived from client.
  * @param which: defines the nth record to send.
  *
  * @retval succes or fail
  */
uint8_t SendEnterNum(uint8_t which, uint8_t checksum);

/**
  * @brief Sends the status of authentication
  *
  *
  * @param checksum: the checksum arrived from client.
  * @param status: status of login state.
  *
  * @retval succes or fail
  */
uint8_t SendStatus(uint8_t status, uint8_t checksum);

/**
  * @brief Sends the maximum number of records that can be stored.
  *
  *
  * @param checksum: the checksum arrived from client.
  *
  * @retval succes or fail
  */
uint8_t SendMaxPassCount(uint8_t checksum);

/**
  * @brief Sends an arbitrary string to client.
  *
  *
  * @param str[]: the string to send.
  * @param checksum: the checksum arrived from client.
  *
  * @retval succes or fail
  */
uint8_t SendString(char str[], uint8_t checksum);

/**
  * @brief Sends arbitrary bytes to client.
  *
  *
  * @param data: the bytes to send.
  * @param length: the length of the array to prevent buffer overflow
  * @param checksum: the checksum arrived from client.
  *
  * @retval succes or fail
  */
uint8_t SendData(uint8_t* data, uint8_t length, uint8_t checksum);

/**
  * @brief Sends an arbitrary byte to client.
  *
  *
  * @param data: the single byte to send.
  *
  * @param checksum: the checksum arrived from client.
  *
  * @retval succes or fail
  */
uint8_t SendSingleData(uint8_t data, uint8_t checksum);

/**
  * @brief Clears the feature buffer.
  *
  */
void ClearNewFeature();

/**
  * @brief Used error detection. The led on the device starts blinking.
  *
  */
void failCom();

/**
  * @brief Creates a record from three specified feature reports.
  *
  * @note First report: record name, number of tabs, number of enters
  *		  Second report: password
  *		  Third report: username
  *
  * @param type: defines if the record is new or used to edit an existing record.
  */
void FeatureToPass(uint8_t type);

/**
  * @brief Method used to handle reports.
  *
  */
void HandleFeatureReport();

/**
  * @brief Only handles specific reports used for user authentication
  *
  * @param masterPassword: stores the password arrived from the client
  * @param status: if the client asks the status
  * @param randBytes: the array of random bytes
  *
  * @retval succes of fail
  *
  */
uint8_t AuthenticateFromFeature(char* masterPassword, uint8_t status, uint8_t *randBytes);

/**
  * @brief Method to send random bytes to the client.
  * @note	This method is used to send random bytes
  *			which are required for the Challenge-
  *			Response authentication.
  *
  *	@param randBytes: the array of random bytes.
  *	@param length: the length of the array (it is a must to prevent buffer overflow).
  *	@param status: if the client asks the status.
  */
void SendRandomLoop(uint8_t *randBytes, int length,  uint8_t status);



#endif
