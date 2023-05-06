#include "main.h"
#include "Communication.h"
#include "Keyboard.h"
#include "PasswordManager.h"
#include "flash_handler.h"
#include "User.h"

uint8_t newfeature[USB_REPORT_SIZE];

uint8_t report_buffer[USB_REPORT_SIZE];		//Variable to receive the report buffer
uint8_t flag = 0;			//Variable to store the button flag
uint8_t flag_rx = 0;			//Variable to store the reception flag

void failCom(){
	for(uint8_t i = 0; i < 10; i++){
		HAL_Delay(150);
		HAL_GPIO_WritePin(Led_GPIO_Port, Led_Pin, RESET);
		HAL_Delay(150);
		HAL_GPIO_WritePin(Led_GPIO_Port, Led_Pin, SET);
	}
	HAL_GPIO_WritePin(Led_GPIO_Port, Led_Pin, SET);
}

void clearReportBuffer(){
	for(uint8_t i = 0; i < USB_REPORT_SIZE-1; i++){
		report_buffer[i] = 0;
	}

}

void ClearNewFeature(){

	for(uint8_t i = 0; i < USB_REPORT_SIZE-1; i++){
		newfeature[i] = 0;
	}
}

uint8_t SendString(char str[], uint8_t checksum){
	USB_Clear_Feature();
	ClearNewFeature();

	uint8_t i;
	for(i = 0; (i < USB_REPORT_MESSAGE_SIZE-1) && (str[i] != '\0'); i++){
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
	//memcpy(newfeature, report_buffer, USB_REPORT_SIZE);
	return USB_Set_Feature(newfeature, 2);
}


uint8_t SendData(uint8_t* data, uint8_t length, uint8_t checksum){
	if(length > USB_REPORT_MESSAGE_SIZE){
		failCom();
		return 0;
	}

	USB_Clear_Feature();
	ClearNewFeature();

	uint8_t i;
	for(i = 0; i < length; i++){
		newfeature[i] = data[i];
	}
	newfeature[i] = '\0';
	newfeature[CHECK_SUM_PLACE] = checksum;
	return USB_Set_Feature(newfeature, length+1);

}


// TODO
//
void SendRandomLoop(uint8_t *randBytes, int length,  uint8_t status)
{
	flag_rx = 0;
	while(1)
	{

		//kliens kerdezi az allapotot
		if(SEND_STATUS == report_buffer[0]){
			SendStatus(status, report_buffer[CHECK_SUM_PLACE]);
		}

		if(report_buffer[0] == SEND_RND_NUM)
		{
			SendData(randBytes, length, report_buffer[CHECK_SUM_PLACE]);
			return;
		}
	}
}

// TODO
/*void ReceiveResponseLoop(char* masterPassword)
{
	flag_rx = 0;
	while(1)
	{
		if(report_buffer[0] == VALID_REPORT)
		{
			int i = 0;
			for(i = 0; i < report_buffer[i] != '\0' && i < USB_REPORT_MESSAGE_SIZE-1; i++)
			{
				masterPassword[i] = report_buffer[i + 1];
			}
			masterPassword[i] = '\0';
			return;
		}
	}
}*/

// TODO
uint8_t AuthenticateFromFeature(char* masterPassword, uint8_t status)
{

	while(1){

		flag_rx = 0;
		while(flag_rx == 0);
		int i;

		//jelszo erkezik
		if(VALID_REPORT == report_buffer[0])
		{
			for(i = 0; (report_buffer[i] != ('\0')) && (i < USB_REPORT_MESSAGE_SIZE-1); i++)
			{
				masterPassword[i] = report_buffer[i + 1];
			}
			masterPassword[i] = '\0';

			return 1;
		}

		//kliens kerdezi az allapotot
		if(SEND_STATUS == report_buffer[0]){
			SendStatus(status, report_buffer[CHECK_SUM_PLACE]);
		}

		//kliens torlest ker
		if(DELETE_ALL == report_buffer[0]){
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


uint8_t SendPassCount(uint8_t checksum){
	return SendSingleData(NumberOfValidRecords(), checksum);

}

uint8_t SendName(uint8_t which, uint8_t checksum){
	char name[USB_REPORT_MESSAGE_SIZE];
	GetNthName(which, name);
	return SendString(name, checksum);
}

uint8_t SendPassword(uint8_t which, uint8_t checksum){
	char password[USB_REPORT_MESSAGE_SIZE];
	GetNthPassword(which, password);
	return SendString(password, checksum);
}

uint8_t SendUsername(uint8_t which, uint8_t checksum){
	char username[USB_REPORT_MESSAGE_SIZE];
	GetNthUsername(which, username);
	return SendString(username, checksum);
}

uint8_t SendTabNum(uint8_t which, uint8_t checksum){
	return SendSingleData(GetNthTabNum(which), checksum);

}

uint8_t SendEnterNum(uint8_t which, uint8_t checksum){
	return SendSingleData(GetNthEnterNum(which), checksum);
}

uint8_t SendMaxPassCount(uint8_t checksum)
{
	return SendSingleData(GetMaxRecordCount(), checksum);
}

void HandleFeatureReport(){
		 //feture report arrived
		  if (flag_rx == 1){
			  switch (report_buffer[0]){

			  	  case SEND_USERNAME:{
					  if(SendUsername(report_buffer[1], report_buffer[CHECK_SUM_PLACE])){
						  LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
			  	//Windows asks for password count
				  case SEND_PASS_COUNT:{
					  if(SendPassCount(report_buffer[CHECK_SUM_PLACE])){
						 LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
				  //Windows asks for a specific password
				  case SEND_PASS:{
					  if(SendPassword(report_buffer[1], report_buffer[CHECK_SUM_PLACE])){
						  LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;

					  break;
				  }
				  //Windows asks for a specific password name
				  case SEND_PASS_NAME:{
					  //report_buffer[1]: Sends which password
					  if(SendName(report_buffer[1], report_buffer[CHECK_SUM_PLACE])){
						  LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
				  //Windows asks for entering password
				  case ENTER_PASS:{
				  	  //report_buffer[1]: Sends which password
					  //report_buffer[2]: Sends additional info (0 = only pass, 1 = only username, 2 = pass + username)
					  if(!WritePass(report_buffer[1], report_buffer[2])){
						  failCom();
					  }
					  break;
				  }
				  //Windows want to teach a new password
				  case ADDING_PASS:{
				  	  //report_buffer[1]: Sends how long is the password
					  //report_buffer: Sends the new password
					  FeatureToPass(report_buffer[0]);
					  flag_rx = 0;
					  LedOn();
					  break;
				  }
				  //Windows want to edit a password
				  case EDIT_PASS:{
				  	  //report_buffer[1]: Sends which password
					  FeatureToPass(report_buffer[0]);
					  flag_rx = 0;
					  LedOn();
					  break;
				  }
				  //Windows want to delete a password
				  case DEL_PASS:{
				  	  //report_buffer[1]: Sends which password
					  if(!DeleteRecord(report_buffer[1])){
						  failCom();
					  }
					  else{
						  LedOn();
					  }
					  break;
				  }
				  case SEND_TAB_NUM:{
					  if(SendTabNum(report_buffer[1], report_buffer[CHECK_SUM_PLACE])){
						 LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
				  case SEND_ENTER_NUM:{
					  if(SendEnterNum(report_buffer[1], report_buffer[CHECK_SUM_PLACE])){
						 LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
				  case SEND_MAX_PASS_COUNT:{
					  if(SendMaxPassCount(report_buffer[CHECK_SUM_PLACE])) {
						  LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
				  //Logout
				  case LOGOUT:{
					  flag_rx = 0;
					  LoginLoop();
					  break;

				  }
				  case SEND_STATUS:{
					  if(SendStatus(LOGGED_IN, report_buffer[CHECK_SUM_PLACE])) {
						  LedOn();
					  }
					  else{
						  failCom();
					  }
					  break;
				  }
				  case DELETE_ALL:{
						flag_rx = 0;
						MassErase();
						LoginLoop();
						break;
				  }
				  //error
				  default:{
					  failCom();
					  break;
				  }
			  }
			  flag_rx = 0;
		  }
 	 }


void FeatureToPass(uint8_t type){
	uint8_t i;

	Record Pass;



	if(type == 6){
		if(NumberOfValidRecords() < report_buffer[1]){
			failCom();
			flag_rx = 0;
			return;
		}
	}
	uint8_t which = report_buffer[1]; // dont care when adding pass
	uint8_t enterNum = report_buffer[2]; // dont care when adding pass
	uint8_t tabNum = report_buffer[3]; // dont care when adding pass

	Pass.tabNum = tabNum;
	Pass.enterNum = enterNum;

	//saveing the name
	for(i = 0; report_buffer[i+4] != '\0'; i++){
		Pass.pageName[i] = report_buffer[i+4];
	}
	Pass.pageName[i] = '\0';

	flag_rx = 0;

	clearReportBuffer();
	while(flag_rx == 0);

	//check for valid input
	if(report_buffer[0] != type){
		failCom();
		flag_rx = 0;
		return;
	}

	//saving the password
	for(i = 0; report_buffer[i+1] != '\0'; i++){
		Pass.password[i] = report_buffer[i+1];
	}
	Pass.password[i] = '\0';
	flag_rx = 0;

	clearReportBuffer();
	//wait for next input
	while(flag_rx == 0);

	//check for valid input
	if(report_buffer[0] != type){
		failCom();
		flag_rx = 0;
		return;
	}

	//saving the username
	for(i = 0; report_buffer[i+1] != '\0'; i++){
		Pass.username[i] = report_buffer[i+1];
	}
	Pass.username[i] = '\0';
	clearReportBuffer();
	//adding password
	if(type == 5){
		if(CreateRecord(&Pass) != F_SUCCESS){
			failCom();
			return;
		}
	}
	//editing password
	if(type == 6){
		if(EditRecord(&Pass, which) != F_SUCCESS){
			failCom();
			return;
		}
	}
}

