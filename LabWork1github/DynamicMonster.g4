grammar DynamicMonster;

definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: healthDeclaration
        | rangeDeclaration
		| damageDeclaration
        | moveDeclaration
        | shootDeclaration
		| ifexpression
		| whileexpression
        ;

nameDeclaration: NAME_T EQUALS name ';' ;
rangeDeclaration: RANGE_T EQUALS NUMBER ';';
healthDeclaration: HEALTH EQUALS NUMBER ';';
moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;
shootDeclaration: SHOOT DIRECTION | SHOOT TO place | SHOOT DIRECTION distanceDeclare | SHOOT DIRECTION damageDeclaration | SHOOT RANDOM
                | SHOOT TO PLAYER | SHOOT TO PLAYER damageDeclaration | SHOOT DIRECTION distanceDeclare damageDeclaration | SHOOT TO place damageDeclaration ;
ifexpression: IF boolexpression block ;
whileexpression: WHILE boolexpression block;
damageDeclaration: DAMAGE COLON NUMBER;


distanceDeclare: DISTANCE COLON NUMBER;
block: BRACKETCLOSE statement* BRACKETCLOSE;
secondnumparam: NUMOPERATION numholder;
numberoperations: numholder secondnumparam* | numholder;
character: PLAYER | ME | TRAP;
secondnumberoperations: COMPARE numberoperations;
booloperation: numberoperations secondnumberoperations? | character ALIVE | character IN RANGE_T ;
secondbooloperation: EXPRESSIONCONNECTER booloperation;
boolsconnected: booloperation secondbooloperation*;
boolexpression: PARENTHESISSTART boolsconnected PARENTHESISCLOSE;
possibleAttributes: HEALTH | PLACE_T | RANGE_T | DAMAGE;
characterAttribute: character ATTRIBUTE possibleAttributes;
numholder: ROUND | NUMBER | characterAttribute;
place: x ',' y;
x: NUMBER;
y: NUMBER;

RANDOM: 'random';
DISTANCE: 'distance';
DAMAGE: 'damage';
DIRECTION: 'F' | 'L' | 'R' | 'B';
NAME_T: 'name';
RANGE_T: 'range';
IN: 'in';
TRAP: 'trap';
PLAYER: 'player';
PLACE_T: 'place';
ROUND: 'round';
ME: 'me';
IF: 'if';
TO: 'to';
WHILE: 'while';
HEALTH: 'HP';
ALIVE: 'alive';
MOVE: 'move';
SHOOT: 'shoot';


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
COMMA: ',';
ATTRIBUTE: '.';
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;