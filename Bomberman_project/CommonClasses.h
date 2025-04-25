#ifndef COMMONCLASSES.h
#define COMMONCLASSES.h

enum Effect{
  Speed,
  FastFuse,
  Ghost,
}
enum InputType{
  ButtonPress,
  Tilt,
}

class User{
  uuid userid;
  char UserName;
  char email
}

class Player{
  char userName;
  int lives;
  PowerUp powerUp;
  bool inLobby;
  int wins;
  int sessionId;
  char charactorColor;
  lobby Lobby;
  bomb Bomb;  
}

class StateManager{
  state State;
}

class State{
  type field;
}

class MenuState{
  type field;
}

class PlayingState{
  Lobby lobby;
}

class Lobby{
  int Id;
  char name;
  uuid HostUserId;
  players list<player>
}

class PowerUp{
  int id;
  char Name;
  Effect effect;
  int duration;
}

class Bomb{
  int Id;
  uuid playersId;
  int sessionId;
  char yCordinate;
  char xCordinate;
  int explosionRadius;
  int fuseTime;
}

class Gyroscope{
  int Id;
  float xCordinate;
  float yCordinate;
  float zCordiante;
}

class Controller{
  int id;
  uuid playerId;
  char playerColor;
  float ledBrightness;
  Gyroscope gyroscope;
  Buttons buttons;
}

class Buttons{
  int Id;
  char buttonName;
}

class InputTime{
  int Id;
  int inputId;
}

class ControllerLog{
  int Id;
  uuid playerId;
  datetime timeStamp;
  InputType inputType;
}

#endif