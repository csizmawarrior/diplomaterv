grammar DynamicMonster;

definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: healthDeclaration
        | rangeDeclaration
        | moveDeclaration
        | shootDeclaration
		| ifexpression
		| whileexpression
        ;

nameDeclaration: NAME_T EQUALS name ';' ;
rangeDeclaration: RANGE_T EQUALS NUMBER ';';
healthDeclaration: HEALTH EQUALS NUMBER ';';
moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION COLON distanceDeclare | MOVE TO PLAYER ;
shootDeclaration: SHOOT DIRECTION | SHOOT TO place | SHOOT DIRECTION COLON distanceDeclare | SHOOT COLON damageDeclare
                | SHOOT TO PLAYER | SHOOT DIRECTION COLON distanceDeclare COLON damageDeclare | SHOOT place COLON damageDeclare ;
ifexpression: IF boolexpression block ;
whileexpression: WHILE boolexpression block;


damageDeclare: DAMAGE COLON NUMBER;
distanceDeclare: DISTANCE COLON NUMBER;
block: BRACKETCLOSE statement* BRACKETCLOSE;
numberoperations: NUMHOLDER NUMOPERATION NUMHOLDER | NUMHOLDER;
character: PLAYER | ME;
booloperation: numberoperations* COMPARE numberoperations* | character ALIVE | PLAYER IN RANGE_T ;
boolsconnected: booloperation EXPRESSIONCONNECTER booloperation;
boolexpression: PARENTHESISSTART boolsconnected PARENTHESISCLOSE;

place: x ',' y;
x: NUMBER;
y: NUMBER;


DISTANCE: 'distance';
DAMAGE: 'damage';
DIRECTION: 'F' | 'L' | 'R' | 'B';
NAME_T: 'name';
RANGE_T: 'range';
IN: 'in';
MOVEROUNDS_T: 'moverounds';
SHOOTROUNDS_T: 'shootrounds';
PLAYER: 'player';
ROUND: 'round';
ME: 'me';
IF: 'if';
TO: 'to';
WHILE: 'while';
HEALTH: 'HP';
ALIVE: 'alive';
NUMHOLDER: HEALTH | ROUND | NUMBER;
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
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;