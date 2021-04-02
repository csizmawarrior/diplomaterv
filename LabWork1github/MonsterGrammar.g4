grammar MonsterGrammar;

definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: shootRoundDeclaration
        | rangeDeclaration
        | moveRoundDeclaration
        ;

nameDeclaration: NAME_T ':' name ';' ;
rangeDeclaration: RANGE_T ':' NUMBER ';';
moveRoundDeclaration: MOVEROUNDS_T ':' NUMBER ';';
shootRoundDeclaration: SHOOTROUNDS_T ':' NUMBER ';';



NAME_T: 'name';
RANGE_T: 'range';
MOVEROUNDS_T: 'moverounds';
SHOOTROUNDS_T: 'shootrounds';


COLON: ':';
SEMI: ';';
COMMA: ',';
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
