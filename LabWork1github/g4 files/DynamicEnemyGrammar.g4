grammar DynamicEnemyGrammar;

/*
 * Parser Rules
 */
 definition: statementList* ;
name: ID;

statementList: nameDeclaration declarations statement*;
declarations: declareStatements* COMMANDS COLON;

statement: declareStatements
		| healDeclaration SEMI
        | moveDeclaration SEMI
        | shootDeclaration SEMI
		| damageDeclaration SEMI
		| teleportDeclaration SEMI
		| spawnDeclaration SEMI
		| ifExpression
		| whileExpression
		| whenExpression
        ;

declareStatements: damageAmountDeclaration SEMI
		| healAmountDeclaration SEMI
		| healthDeclaration SEMI
		| teleportPointDeclaration SEMI
		| spawnPointDeclaration SEMI
		| spawnTypeDeclaration SEMI
		;

nameDeclaration: trapNameDeclaration | monsterNameDeclaration ;
trapNameDeclaration: TRAP NAME_T EQUALS name SEMI ;
monsterNameDeclaration: MONSTER NAME_T EQUALS name SEMI ;
healthDeclaration: HEALTH EQUALS NUMBER ;
healAmountDeclaration: HEAL EQUALS NUMBER;
damageAmountDeclaration: DAMAGE EQUALS NUMBER;
hpChangeAmountDeclaration: damageAmountDeclaration | healAmountDeclaration;
teleportPointDeclaration: TELEPORT_PLACE EQUALS place;
spawnPointDeclaration: SPAWN_PLACE EQUALS place;
spawnTypeDeclaration: SPAWN_TYPE EQUALS name;

distanceDeclare: DISTANCE EQUALS NUMBER;

moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;

healthChangeOption: DIRECTION | DIRECTION distanceDeclare | DIRECTION hpChangeAmountDeclaration | DIRECTION distanceDeclare hpChangeAmountDeclaration |
					TO place | TO place hpChangeAmountDeclaration | TO character | TO character hpChangeAmountDeclaration | RANDOM;
shootDeclaration: SHOOT healthChangeOption;
damageDeclaration: DAMAGE healthChangeOption ;
healDeclaration: HEAL healthChangeOption;
spawnDeclaration: SPAWN MONSTER name TO place | SPAWN RANDOM | SPAWN MONSTER name | SPAWN TO place | SPAWN;
teleportDeclaration: TELEPORT_T character TO place | TELEPORT_T character RANDOM | TELEPORT_T character;
ifExpression: IF PARENTHESISSTART boolExpression PARENTHESISCLOSE block ;
whileExpression: WHILE PARENTHESISSTART boolExpression PARENTHESISCLOSE block;
whenExpression:  WHEN PARENTHESISSTART triggerEvent PARENTHESISCLOSE block;


triggerEvent: character action | PLAYER HEALTH_CHECK;

action: MOVE fromPlace? TO place | DIE | SHOOT (NUMBER)? TO (character | place) | DAMAGE (NUMBER)? TO (character | place) |
			HEAL (NUMBER)? TO (character | place) | TELEPORT_T character TO place | SPAWN character TO place;

block: bracketStartCommand statement* bracketCloseCommand;
bracketStartCommand: BRACKETSTART;
bracketCloseCommand: BRACKETCLOSE;
character: PLAYER | ME | TRAP | MONSTER | PARTNER;

possibleAttributes: name | possibleAttributes DOT possibleAttributes | TELEPORT_PLACE | PLACE_T | SPAWN_PLACE | SPAWN_TYPE | ROUND
                | HEALTH | HEAL | RANDOM | DAMAGE | DISTANCE | NAME_T | TRAP | MONSTER | ME | PLAYER | PARTNER;

place: x ',' y;
x: NUMBER;
y: NUMBER;
fromPlace: FROM place;

boolExpression: PARENTHESISSTART boolExpression PARENTHESISCLOSE nextBoolExpression? | NEGATE boolExpression nextBoolExpression?
            | numberExpression numToBoolOperation numberExpression nextBoolExpression? | functionExpression nextBoolExpression?;
nextBoolExpression: BOOLCONNECTER boolExpression;

numberExpression: PARENTHESISSTART numberExpression PARENTHESISCLOSE | ABSOLUTESTART numberExpression PARENTHESISCLOSE |
                      numberExpression NUMCONNECTERMULTIP numberExpression | numberExpression NUMCONNECTERADD numberExpression |
						something;
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
HEALTH_CHECK: 'health_check';
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
COMMANDS: 'commands';
WHILE: 'while';
HEALTH: 'health';
ALIVE: 'alive';
MOVE: 'move';
ON:'on';
SHOOT: 'shoot';
SPAWN: 'spawn';
TELEPORT_T: 'teleport';
PARTNER: 'partner';
HEAL: 'heal';
FROM: 'from';
WHEN: 'when';
DIE: 'die';
STAY: 'stay';
AT: 'at';

EQUALS: '=' ;
ABSOLUTESTART: ('Abs'PARENTHESISSTART);
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