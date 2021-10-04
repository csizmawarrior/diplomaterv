grammar DynamicTrap;


definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: damageDeclaration ';'
        | moveDeclaration ';'
		| ifexpression ';'
		| whileexpression ';'
		| damageDeclare ';' 
		| healDeclare ';'
        ;

insideBlock: healthDeclaration ';'
		| damageDeclaration ';'
        | moveDeclaration ';'
        | shootDeclaration ';'
		;

damageDeclaration: DAMAGE DIRECTION | DAMAGE DIRECTION damageDeclare | DAMAGE DIRECTION distanceDeclare | DAMAGE DIRECTION distanceDeclare damageDeclare
					| DAMAGE RANDOM	| DAMAGE TO place | DAMAGE TO place damageDeclare | DAMAGE TO character | DAMAGE TO character damageDeclare ;
healDeclaration: HEAL DIRECTION | HEAL DIRECTION healDeclare | HEAL DIRECTION distanceDeclare | HEAL DIRECTION distanceDeclare healDeclare
					| HEAL RANDOM	| HEAL TO place | HEAL TO place healDeclare | HEAL TO character | HEAL TO character healDeclare ;
spawnDeclaration: SPAWN MONSTER name TO place | SPAWN RANDOM;
teleport: TELEPORT_T character TO place | TELEPORT_T character RANDOM;
moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;
nameDeclaration: NAME_T EQUALS name ';' ;
damageDeclare: DAMAGE EQUALS NUMBER;
healDeclare: HEAL EQUALS NUMBER;
distanceDeclare: DISTANCE EQUALS NUMBER;
ifexpression: IF boolexpression block ;
whileexpression: WHILE boolexpression block | WHILE boolexpression statement;

block: BRACKETCLOSE insideBlock* BRACKETCLOSE;
firstnumparam : numholder;
secondnumparam: NUMOPERATION numholder;
numberoperations: numholder secondnumparam*;
character: PLAYER | ME | MONSTER | TRAP;
booloperation: numberoperations COMPARE numberoperations | character ALIVE | character IS NEAR character IS ON ME;
secondbooloperation: EXPRESSIONCONNECTER booloperation;
boolsconnected: booloperation secondbooloperation*;
boolexpression: PARENTHESISSTART boolsconnected PARENTHESISCLOSE;
possibleAttributes: HEALTH | PLACE_T ATTRIBUTE X | PLACE_T ATTRIBUTE Y | DAMAGE | HEAL;
characterAttribute: character ATTRIBUTE possibleAttributes;
numholder: ROUND | NUMBER | characterAttribute | ABSOLUTE numholder ABSOLUTE;
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
NEAR: 'near';
IS: 'is';
ON: 'ON';
WHILE: 'while';
HEALTH: 'HP';
ALIVE: 'alive';
MOVE: 'move';
SPAWN: 'spawn';
TELEPORT_T: 'teleport';
HEAL: 'heal';
NAME_T: 'name';
PLAYER: 'player';
EFFECT_T: 'effect';

EQUALS: '=' ;
ABSOLUTE: '|';
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
