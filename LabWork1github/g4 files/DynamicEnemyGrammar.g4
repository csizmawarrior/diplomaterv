grammar DynamicEnemyGrammar;

/*
 * Parser Rules
 */
 definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: damageAmountDeclaration ';'
		| healAmountDeclaration ';'
		| teleportPointDeclaration ';'
		| spawnPointDeclaration ';'
		| spawnTypeDeclaration ';'
		| healthDeclaration ';'
		| healDeclaration ';'
        | moveDeclaration ';'
        | shootDeclaration ';'
		| ifexpression 
		| whileexpression 
		| damageDeclaration ';'
		| teleportDeclaration ';'
		| spawnDeclaration ';'
        ;

nameDeclaration: trapNameDeclaration | monsterNameDeclaration ;
trapNameDeclaration: TRAP NAME_T EQUALS name ';' ;
monsterNameDeclaration: MONSTER NAME_T EQUALS name ';' ;

healthDeclaration: HEALTH EQUALS NUMBER ;
healAmountDeclaration: HEAL EQUALS NUMBER;
damageAmountDeclaration: DAMAGE EQUALS NUMBER ;
teleportPointDeclaration: TELEPORT_PLACE EQUALS place;
spawnPointDeclaration: SPAWN_PLACE EQUALS place;
spawnTypeDeclaration: SPAWN_TYPE EQUALS name;

distanceDeclare: DISTANCE EQUALS NUMBER;

moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;
shootDeclaration: SHOOT DIRECTION | SHOOT TO place | SHOOT DIRECTION distanceDeclare | SHOOT DIRECTION damageAmountDeclaration | SHOOT RANDOM
                | SHOOT TO PLAYER | SHOOT TO PLAYER damageAmountDeclaration | SHOOT DIRECTION distanceDeclare damageAmountDeclaration | SHOOT TO place damageAmountDeclaration ;
damageDeclaration: DAMAGE DIRECTION | DAMAGE DIRECTION damageAmountDeclaration | DAMAGE DIRECTION distanceDeclare | DAMAGE DIRECTION distanceDeclare damageAmountDeclaration
					| DAMAGE RANDOM	| DAMAGE TO place | DAMAGE TO place damageAmountDeclaration | DAMAGE TO character | DAMAGE TO character damageAmountDeclaration ;
healDeclaration: HEAL DIRECTION | HEAL DIRECTION healAmountDeclaration | HEAL DIRECTION distanceDeclare | HEAL DIRECTION distanceDeclare healAmountDeclaration
					| HEAL RANDOM	| HEAL TO place | HEAL TO place healAmountDeclaration | HEAL TO character | HEAL TO character healAmountDeclaration ;
spawnDeclaration: SPAWN MONSTER name TO place | SPAWN RANDOM | SPAWN MONSTER name | SPAWN TO place | SPAWN;
teleportDeclaration: TELEPORT_T character TO place | TELEPORT_T character RANDOM;
ifexpression: IF PARENTHESISSTART boolExpression PARENTHESISCLOSE block ;
whileexpression: WHILE PARENTHESISSTART boolExpression PARENTHESISCLOSE block;

block: BRACKETSTART statement* BRACKETCLOSE;
character: PLAYER | ME | TRAP | MONSTER;

possibleAttributes: name | possibleAttributes DOT possibleAttributes | TELEPORT_PLACE | PLACE_T | SPAWN_PLACE | SPAWN_TYPE | ROUND
                | HEALTH | HEAL | RANDOM | DAMAGE | DISTANCE | NAME_T | TRAP | MONSTER | ME | PLAYER;

place: x ',' y;
x: NUMBER;
y: NUMBER;

boolExpression: PARENTHESISSTART boolExpression PARENTHESISCLOSE nextBoolExpression? | NEGATE boolExpression nextBoolExpression?
            | numberExpression numToBoolOperation numberExpression nextBoolExpression? | functionExpression nextBoolExpression? | attribute COMPARE attribute nextBoolExpression?;
nextBoolExpression: BOOLCONNECTER boolExpression;

numberExpression: numberMultipExpression (NUMCONNECTERADD numberMultipExpression)*;
numberMultipExpression: numberFirstExpression (NUMCONNECTERMULTIP numberFirstExpression)*;
numberFirstExpression: PARENTHESISSTART numberExpression PARENTHESISCLOSE | ABSOLUTE numberExpression ABSOLUTE | something;
functionExpression: character function;

something: NUMBER | ROUND | attribute;
attribute: character DOT possibleAttributes;
numToBoolOperation: NUMCOMPARE | COMPARE;
function: IS ALIVE | IS NEAR ;
/*
 * Lexer Rules
 */
TELEPORT_PLACE: 'teleport_place';
SPAWN_PLACE: 'spawn_place';
SPAWN_TYPE: 'spawn_type';
RANDOM: 'random';
DISTANCE: 'distance';
DAMAGE: 'damage';
DIRECTION: 'F' | 'L' | 'R' | 'B';
NAME_T: 'name';
TRAP: 'trap';
MONSTER: 'monster';
PLAYER: 'player';
PLACE_T: 'place';
ROUND: 'round';
NEAR: 'near';
IS: 'is';
ME: 'me';
IF: 'if';
TO: 'to';
WHILE: 'while';
HEALTH: 'health';
ALIVE: 'alive';
MOVE: 'move';
ON:'on';
SHOOT: 'shoot';
SPAWN: 'spawn';
TELEPORT_T: 'teleport';
HEAL: 'heal';

EQUALS: '=' ;
ABSOLUTE: '|';
NEGATE: '!';
BOOLCONNECTER: '||' | '&&' ;
COMPARE: '==' | '!=' ;
NUMCOMPARE: '<' | '>' ;
NUMCONNECTERMULTIP: '*' | '/' | '%' ;
NUMCONNECTERADD: '+' | '-' ;
PARENTHESISSTART: '(';
PARENTHESISCLOSE: ')';
BRACKETCLOSE: '}';
BRACKETSTART: '{';
COLON: ':';
SEMI: ';';
COMMA: ',';
NUMBER: [0-9]+ (DOT[0-9]+)?;
DOT: '.';
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;