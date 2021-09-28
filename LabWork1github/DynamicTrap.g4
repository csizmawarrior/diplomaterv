grammar DynamicTrap;


definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: damageDeclaration
        | rangeDeclare
        | moveDeclaration
		| ifexpression
		| whileexpression
		| damageDeclare | healDeclare
		| rangeDeclare
        ;

//TODO: check if all cases are available or not

damageDeclaration: DAMAGE DIRECTION | DAMAGE DIRECTION damageDeclare | DAMAGE DIRECTION distanceDeclare | DAMAGE DIRECTION distanceDeclare damageDeclare
					| DAMAGE RANDOM	| DAMAGE TO place | DAMAGE TO place damageDeclare | DAMAGE TO character | DAMAGE TO character damageDeclare ;
healDeclaration: HEAL DIRECTION | HEAL DIRECTION damageDeclare | HEAL DIRECTION distanceDeclare | HEAL DIRECTION distanceDeclare damageDeclare
					| HEAL RANDOM	| HEAL TO place | HEAL TO place damageDeclare | HEAL TO character | HEAL TO character damageDeclare ;
spawnDeclaration: SPAWN MONSTER name TO place | SPAWN RANDOM;
teleport: TELEPORT_T character TO place | TELEPORT_T character RANDOM;
moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION COLON distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;
rangeDeclare: RANGE_T ':' NUMBER;
nameDeclaration: NAME_T ':' name ';' ;
damageDeclare: DAMAGE COLON NUMBER;
healDeclare: HEAL COLON NUMBER;
distanceDeclare: DISTANCE COLON NUMBER;
ifexpression: IF boolexpression block ;
whileexpression: WHILE boolexpression block;

block: BRACKETCLOSE statement* BRACKETCLOSE;
firstnumparam : numholder;
secondnumparam: NUMOPERATION numholder;
numberoperations: numholder secondnumparam* | numholder;
character: PLAYER | ME | TRAP;
secondnumberoperations: COMPARE numberoperations;
booloperation: numberoperations secondnumberoperations? | character ALIVE | character IN RANGE_T ;
secondbooloperation: EXPRESSIONCONNECTER booloperation;
boolsconnected: booloperation secondbooloperation*;
boolexpression: PARENTHESISSTART boolsconnected PARENTHESISCLOSE;
possibleAttributes: HEALTH | PLACE_T | RANGE_T | DAMAGE | HEAL;
characterAttribute: character ATTRIBUTE possibleAttributes;
numholder: ROUND | NUMBER | characterAttribute;
place: x ',' y;
x: NUMBER;
y: NUMBER;

DISTANCE: 'distance';
DAMAGE: 'damage';
DIRECTION: 'F' | 'L' | 'R' | 'B';
IN: 'in';
TRAP: 'trap';
MONSTER: 'monster';
ROUND: 'round';
ME: 'me';
IF: 'if';
RANDOM: 'random';
TO: 'to';
PLACE_T: 'place';
WHILE: 'while';
HEALTH: 'HP';
ALIVE: 'alive';
MOVE: 'move';
SPAWN: 'spawn';
TELEPORT_T: 'teleport';
HEAL: 'heal';
RANGE_T: 'range';
NAME_T: 'name';
PLAYER: 'player';
EFFECT_T: 'effect';

EQUALS: '=' ;
EXPRESSIONCONNECTER: '||' | '&&' ;
COMPARE: '<' | '>' | '==' | '!=' ;
NUMOPERATION: '+' | '-' | '*' | '/' | '%' ;
PARENTHESISSTART: '(';
PARENTHESISCLOSE: ')';
BRACKETCLOSE: '}';
BRACKETSTART: '{';
COLON: ':';
SEMI: ';';
ATTRIBUTE: '.';
COMMA: ',';
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
