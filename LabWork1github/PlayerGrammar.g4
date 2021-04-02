/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

grammar PlayerGrammar;

program: statementList;
statementList: statement*;
statement: movingStatement
        | shootingStatement
        | healthCheckStatement
        ;

direction: 'F' | 'L' | 'R' | 'B';
movingStatement: MOVE direction;
shootingStatement: SHOOT direction;
healthCheckStatement: HEALTH;


SEMI: ';';
COMMA: ',';
FORWARD: 'F';
LEFT: 'L';
RIGHT: 'R';
BACKWARD: 'B';

MOVE: 'move';
SHOOT: 'shoot';
HEALTH: 'health';

WS: (' ' | '\t' | '\n' | '\r') -> skip;