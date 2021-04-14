grammar TrapGrammar;

definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: effectDeclaration
        | rangeDeclaration
        | moveData
        ;



nameDeclaration: NAME ':' name ';' ;
effectDeclaration: EFFECT_T ':' effect ';';
moveData: rangeDeclaration',' moveRoundDeclaration ';';


effect: damage | heal | teleport | monsterSpawn;
damage: DAMAGE NUMBER;
heal: HEAL NUMBER;
monsterSpawn: SPAWN place;
teleport: TELEPORT_T place;
rangeDeclaration: RANGE ':' NUMBER ';';
moveRoundDeclaration: MOVEROUNDS ':' NUMBER;
place: x ',' y;

x: NUMBER;
y: NUMBER;

SPAWN: 'spawn';
TELEPORT_T: 'teleport';
HEAL: 'heal';
DAMAGE : 'damage';
RANGE: 'range';
NAME: 'name';
EFFECT_T: 'effect';
MOVEROUNDS: 'moverounds';

COLON: ':';
SEMI: ';';
COMMA: ',';
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;