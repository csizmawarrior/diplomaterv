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
ifexpression: IF PARENTHESISSTART expression PARENTHESISCLOSE block ;
whileexpression: WHILE PARENTHESISSTART expression PARENTHESISCLOSE block | WHILE PARENTHESISSTART expression PARENTHESISCLOSE statement;

block: BRACKETCLOSE statement* BRACKETCLOSE;
character: PLAYER | ME | TRAP | MONSTER;
possibleAttributes: HEALTH | PLACE_T ATTRIBUTE X | PLACE_T ATTRIBUTE Y | DAMAGE;


place: x ',' y;
x: NUMBER;
y: NUMBER;

expression: expression operation expression | PARENTHESISSTART expression PARENTHESISCLOSE | ABSOLUTE expression ABSOLUTE | something | NEGATE expression;
something: character | NUMBER | ROUND | possibleAttributes | NOTHING;

operation: ATTRIBUTE | NUMCONNECTER | BOOLCONNECTER | COMPARE | ALIVE | IS NEAR;


NOTHING: 'nothing';
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
NEGATE: '!';
BOOLCONNECTER: '||' | '&&' ;
COMPARE: '<' | '>' | '==' | '!=' ;
NUMCONNECTER: '+' | '-' | '*' | '/' | '%' ;
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