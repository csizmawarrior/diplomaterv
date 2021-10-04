grammar DynamicMonster;

definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: healthDeclaration ';'
		| damageDeclaration ';'
        | moveDeclaration ';'
        | shootDeclaration ';'
		| ifexpression ';'
		| whileexpression ';'
        ;

nameDeclaration: NAME_T EQUALS name ';' ;
healthDeclaration: HEALTH EQUALS NUMBER ;
damageDeclaration: DAMAGE EQUALS NUMBER ;
distanceDeclare: DISTANCE EQUALS NUMBER;
moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;
shootDeclaration: SHOOT DIRECTION | SHOOT TO place | SHOOT DIRECTION distanceDeclare | SHOOT DIRECTION damageDeclaration | SHOOT RANDOM
                | SHOOT TO PLAYER | SHOOT TO PLAYER damageDeclaration | SHOOT DIRECTION distanceDeclare damageDeclaration | SHOOT TO place damageDeclaration ;
ifexpression: IF boolexpression block ;
whileexpression: WHILE boolexpression block | WHILE boolexpression statement;

block: BRACKETCLOSE statement* BRACKETCLOSE;
numholder: ROUND | NUMBER | characterAttribute | ABSOLUTE numholder ABSOLUTE;
secondnumparam: NUMOPERATION numholder;
numberoperations: numholder secondnumparam*;
booloperation: numberoperations COMPARE numberoperations | character ALIVE | character IS NEAR;
secondbooloperation: EXPRESSIONCONNECTER booloperation;
boolsconnected: booloperation secondbooloperation*;
boolexpression: PARENTHESISSTART boolsconnected PARENTHESISCLOSE;
character: PLAYER | ME | TRAP | MONSTER;
possibleAttributes: HEALTH | PLACE_T ATTRIBUTE X | PLACE_T ATTRIBUTE Y | DAMAGE;
characterAttribute: character ATTRIBUTE possibleAttributes;

place: x ',' y;
x: NUMBER;
y: NUMBER;

RANDOM: 'random';
DISTANCE: 'distance';
DAMAGE: 'damage';
DIRECTION: 'F' | 'L' | 'R' | 'B';
NAME_T: 'name';
IN: 'in';
TRAP: 'trap';
PLAYER: 'player';
PLACE_T: 'place';
ROUND: 'round';
NEAR: 'near';
IS: 'is';
ME: 'me';
IF: 'if';
TO: 'to';
WHILE: 'while';
HEALTH: 'HP';
ALIVE: 'alive';
MOVE: 'move';
SHOOT: 'shoot';


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
COMMA: ',';
ATTRIBUTE: '.';
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;