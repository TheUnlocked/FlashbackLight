WRD File Format Specification

## Layout:
| Block			| Offset |
| ------------- | ------- |
| Header		| 0x00	|
| Code			| 0x20	|
| Branch Labels	| Code End Ptr	|
| Labels		| Branch Labels End Ptr	|
| Parameters	| Labels End Ptr	|
| Strings		| Params End Ptr	|

_Every block ends when the next block begins._


## Header:
### Total 32 Bytes
| Description			| Type		| Additional Information |
| ---------------------	| -----	| -------------------------- |
| String Count			| ushort | |
| Labels Count			| ushort | |
| Params Count			| ushort | |
| Branch Label Count	| ushort | |
| 00 00 00 00			| 4 bytes | |
| Code End Ptr			| uint	| |
| Branch Labels End Ptr	| uint	| | 
| Labels End Ptr		| uint	| |
| Params End Ptr		| uint	| |
| String End Ptr		| uint	| Use 0 if strings are not included in the WRD |

## Code:
Format: `(0x70 opcode arg*)*`  
All opcodes are one byte.  
All args are two bytes, in big endian notation.  

## Branch Labels:
### Associates numbers with code offsets for LBN (Label Branch Number) opcodes.  
Format: `(number offset)*`  
`number`: ushort  
`offset`: ushort  
The offset is relative to the start of the Code block.

## Labels:
Format: `(length label 0x00)*`  
`length`: byte  
`label`: ASCII string  
Length refers to the length of the label string.

## Parameters:
TODO

## Strings:
TODO
