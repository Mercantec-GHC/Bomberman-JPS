
#ifndef COMMONCLASSES.h
#define COMMONCLASSES.h

#include <UUID.h>

enum Effect {
  Speed,
  FastFuse,
  Ghost
};

enum InputType {
  ButtonPress,
  Tilt
};

// Forward declarations
class Lobby;
class Bomb;
class PowerUp;
class State;
class MenuState;
class PlayingState;

class User {
public:
  UUID userid;
  char UserName[32]; 
  char email[64];
};

class Player {
public:
  char userName[32];
  int lives;
  PowerUp* powerUp;
  bool inLobby;
  int wins;
  int sessionId;
  char characterColor;
  Lobby* lobby;
  Bomb* bomb;
};

class StateManager {
public:
  State* state;
};

class State {
public:
  int field;
};

class MenuState : public State {
public:
  int field;
};

class PlayingState : public State {
public:
  Lobby* lobby;
};

class Lobby {
public:
  int id;
  char name[32];
  UUID hostUserId;
  Player players[4]; //max 4 players
};

class PowerUp {
public:
  int id;
  char name[32];
  Effect effect;
  int duration;
};

class Bomb {
public:
  int id;
  UUID playerId;
  int sessionId;
  char yCoordinate;
  char xCoordinate;
  int explosionRadius;
  int fuseTime;
};

class Gyroscope {
public:
  int id;
  float xCoordinate;
  float yCoordinate;
  float zCoordinate;
};

class Buttons {
public:
  int id;
  char buttonName;
};

class Controller {
public:
  int id;
  UUID playerId;
  char playerColor;
  float ledBrightness;
  Gyroscope gyroscope;
  Buttons buttons;
};

class InputTime {
public:
  int id;
  int inputId;
};

class ControllerLog {
public:
  int id;
  UUID playerId;
  unsigned long timeStamp; 
  InputType inputType;
};

#endif