#include "main.h"
#include "Communication.h"
#include "Keyboard.h"
#include "PasswordManager.h"
#include "flash_handler.h"
#include "User.h"

uint8_t newfeature[USB_REPORT_SIZE];

uint8_t report_buffer[USB_REPORT_SIZE];		//Variable to receive the report buffer
uint8_t flag = 0;							//Variable to store the button flag
uint8_t flag_rx = 0;						//Variable to store the reception flag
uint8_t authenticated = 0; 					// Variable for user status

void failCom()
{
	for(uint8_t i = 0; i < 10; i++)
	{
		HAL_Delay(150);
		HAL_GPIO_WritePin(Led_GPIO_Port, Led_Pin, RESET);
		HAL_Delay(150);
		HAL_GPIO_WritePin(Led_GPIO_Port, Led_Pin, SET);
	}
	HAL_GPIO_WritePin(Led_GPIO_Port, Led_Pin, SET);
}

void clearReportBuffer()
{
	for(uint8_t i = 0; i < USB_REPORT_SIZE-1; i++)
	{
		report_buffer[i] = 0;
	}

}

void ClearNewFeature()
{
	for(uint8_t i = 0; i < USB_REPORT_SIZE-1; i++)
	{
		newfeature[i] = 0;
	}
}

uint8_t SendString(char str[], uint8_t checksum)
{
	USB_Clear_Feature();
	ClearNewFeature();

	uint8_t i;
	for(i = 0; (i < USB_REPORT_MESSAGE_SIZE-1) && (str[i] != '\0'); i++)
	{
		newfeature[i] = str[i];
	}
	newfeature[i] = '\0';

	newfeature[CHECK_SUM_PLACE] = checksum;

	return USB_Set_Feature(newfeature, i);

}

uint8_t SendSingleData(uint8_t data, uint8_t checksum)
{
	USB_Clear_Feature();
	ClearNewFeature();

	newfeature[0] = data;
	newfeature[1] = '\0';
	newfeature[CHECK_SUM_PLACE] = checksum;

	return USB_Set_Feature(newfeature, 2);
}


uint8_t SendData(uint8_t* data, uint8_t length, uint8_t checksum)
{
	if(length > USB_REPORT_MESSAGE_SIZE)
	{
		failCom();
		return 0;
	}

	USB_Clear_Feature();
	ClearNewFeature();

	uint8_t i;
	for(i = 0; i < length; i++)
	{
		newfeature[i] = data[i];
	}
	newfeature[i] = '\0';
	newfeature[CHECK_SUM_PLACE] = checksum;
	return USB_Set_Feature(newfeature, length+1);

}


void SendRandomLoop(uint8_t *randBytes, int length,  uint8_t status)
{
	flag_rx = 0;
	while(1)
	{

		// If client asks for authentication status then the device sends it here.
		if(SEND_STATUS == report_buffer[0])
		{
			SendStatus(status, report_buffer[CHECK_SUM_PLACE]);
			flag_rx = 0;
		}

		// If random bytes needed for HMAC, the device sends them.
		if(report_buffer[0] == SEND_RND_NUM)
		{
			SendData(randBytes, length, report_buffer[CHECK_SUM_PLACE]);
			flag_rx = 0;
			return;
		}
	}
}


uint8_t AuthenticateFromFeature(char* masterPassword, uint8_t status, uint8_t *randBytes)
{

	while(1)
	{

		flag_rx = 0;
		while(flag_rx == 0);
		int i;

		// Client sends the response to try a login or the master password to store.
		if(VALID_REPORT == report_buffer[0])
		{
			for(i = 0; (report_buffer[i] != ('\0')) && (i < USB_REPORT_MESSAGE_SIZE-1); i++)
			{
				masterPassword[i] = report_buffer[i + 1];
			}
			masterPassword[i] = '\0';

			return 1;
		}

		// If client asks for authentication status then the device sends it here.
		if(SEND_STATUS == report_buffer[0])
		{
			SendStatus(status, report_buffer[CHECK_SUM_PLACE]);
		}

		// If random bytes needed for HMAC, the device sends them.
		if(report_buffer[0] == SEND_RND_NUM)
		{
			SendData(randBytes, 13, report_buffer[CHECK_SUM_PLACE]);
		}


		// If client asks a mass delete the device handle it here.
		if(DELETE_ALL == report_buffer[0])
		{
			MassErase();
			LoginLoop();
		}
	}
	flag_rx = 0;
	return 0;
}


uint8_t SendStatus(uint8_t status, uint8_t checksum)
{
	return SendSingleData(status, checksum);
}


uint8_t SendPassCount(uint8_t checksum)
{
	return SendSingleData(NumberOfValidRecords(), checksum);

}

uint8_t SendName(uint8_t which, uint8_t checksum)
{
	char name[USB_REPORT_MESSAGE_SIZE];
	GetNthName(which, name);
	return SendString(name, checksum);
}

uint8_t SendPassword(uint8_t which, uint8_t checksum)
{
	char password[USB_REPORT_MESSAGE_SIZE];
	GetNthPassword(which, password);
	return SendString(password, checksum);
}

uint8_t SendUsername(uint8_t which, uint8_t checksum)
{
	char username[USB_REPORT_MESSAGE_SIZE];
	GetNthUsername(which, username);
	return SendString(username, checksum);
}

uint8_t SendTabNum(uint8_t which, uint8_t checksum)
{
	return SendSingleData(GetNthTabNum(which), checksum);

}

uint8_t SendEnterNum(uint8_t which, uint8_t checksum)
{
	return SendSingleData(GetNthEnterNum(which), checksum);
}

uint8_t SendMaxPassCount(uint8_t checksum)
{
	return SendSingleData(GetMaxRecordCount(), checksum);
}

void HandleFeatureReport()
{
		// Check if user is already logged in
		if(!authenticated)
		{
			LedOff();
			LoginLoop();
		}

		if(authenticated)
		{
			LedOn();
			if (flag_rx == 1)
			{
				  // The first element of the report buffer defines the command.
				  // Report buffer second element defines the selected password.
				  switch (report_buffer[0])
				  {

					  // Client asks for the username of the selected record.
					  case SEND_USERNAME:
					  {

						  if(!SendUsername(report_buffer[1], report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }
					 // Client asks for the number of records stored on the device.
					  case SEND_PASS_COUNT:
					  {
						  if(!SendPassCount(report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }
					  // Client asks for the password of the selected record.
					  case SEND_PASS:
					  {

						  if(!SendPassword(report_buffer[1], report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }
					  // Client asks for the name of the selected record.
					  case SEND_PASS_NAME:
					  {
						  //report_buffer[1]: Sends which password
						  if(!SendName(report_buffer[1], report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }
					  //Client asks for entering password
					  case ENTER_PASS:
					  {
						  //report_buffer[1]: Sends which password
						  //report_buffer[2]: Sends additional info (0 = only pass, 1 = only username, 2 = pass + username)
						  if(!WritePass(report_buffer[1], report_buffer[2]))
						  {
							  failCom();
						  }
						  break;
					  }
					  //Client wants to store a new password.
					  case ADDING_PASS:
					  {
						  //report_buffer[1]: Sends how long is the password
						  //report_buffer: Sends the new password
						  FeatureToPass(report_buffer[0]);
						  flag_rx = 0;

						  break;
					  }
					  //Client wants to edit a new password.
					  case EDIT_PASS:
					  {
						  //report_buffer[1]: Sends which password
						  FeatureToPass(report_buffer[0]);
						  flag_rx = 0;

						  break;
					  }
					  //Client wants to delete a password.
					  case DEL_PASS:
					  {
						  //report_buffer[1]: Sends which password
						  if(!DeleteRecord(report_buffer[1]))
						  {
							  failCom();
						  }

						  break;
					  }

					  // Client asks for the number of tabs of the selected record.
					  case SEND_TAB_NUM:
					  {
						  if(!SendTabNum(report_buffer[1], report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }

					  // Client asks for the number of enters of the selected record.
					  case SEND_ENTER_NUM:
					  {
						  if(!SendEnterNum(report_buffer[1], report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }

					  // Client asks for the maximum number of records that can be stored on the device.
					  case SEND_MAX_PASS_COUNT:
					  {
						  if(!SendMaxPassCount(report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }

					  // Client asks for logout.
					  case LOGOUT:
					  {
						  flag_rx = 0;
						  authenticated = 0;
						  LoginLoop();
						  break;

					  }

					  // Client asks for authentication status.
					  case SEND_STATUS:
					  {
						  if(!SendStatus(LOGGED_IN, report_buffer[CHECK_SUM_PLACE]))
						  {
							  failCom();
						  }

						  break;
					  }

					  // Client asks for mass delete.
					  case DELETE_ALL:
					  {
							flag_rx = 0;
							MassErase();
							LoginLoop();
							break;
					  }

					  // If invalid command arrives.
					  default:
					  {
						  failCom();
						  break;
					  }
				  }

				  flag_rx = 0;
			}
		}
 	 }


void FeatureToPass(uint8_t type)
{
	uint8_t i;

	Record Pass;

	if(type == 6)
	{
		if(NumberOfValidRecords() < report_buffer[1])
		{
			failCom();
			flag_rx = 0;
			return;
		}
	}
	uint8_t which = report_buffer[1]; 		// defines which password to edit (when adding a record it is a don't care)
	uint8_t enterNum = report_buffer[2];
	uint8_t tabNum = report_buffer[3];

	Pass.tabNum = tabNum;
	Pass.enterNum = enterNum;

	//saving the name of the record.
	for(i = 0; report_buffer[i+4] != '\0'; i++)
	{
		Pass.pageName[i] = report_buffer[i+4];
	}

	Pass.pageName[i] = '\0';

	flag_rx = 0;

	clearReportBuffer();

	// waiting for the password of the record.
	while(flag_rx == 0);

	//check for valid input
	if(report_buffer[0] != type)
	{
		failCom();
		flag_rx = 0;
		return;
	}

	//saving the password
	for(i = 0; report_buffer[i+1] != '\0'; i++)
	{
		Pass.password[i] = report_buffer[i+1];
	}
	Pass.password[i] = '\0';
	flag_rx = 0;

	clearReportBuffer();

	//waiting for the username of the record
	while(flag_rx == 0);

	//check for valid input
	if(report_buffer[0] != type)
	{
		failCom();
		flag_rx = 0;
		return;
	}

	//saving the username
	for(i = 0; report_buffer[i+1] != '\0'; i++)
	{
		Pass.username[i] = report_buffer[i+1];
	}

	Pass.username[i] = '\0';
	clearReportBuffer();

	//adding record
	if(type == 5)
	{
		if(CreateRecord(&Pass) != F_SUCCESS)
		{
			failCom();
			return;
		}
	}

	//editing record
	if(type == 6)
	{
		if(EditRecord(&Pass, which) != F_SUCCESS)
		{
			failCom();
			return;
		}
	}
}

