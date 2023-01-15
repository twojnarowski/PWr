.data
	factorial: .word 1 #result
	factor: .word 5   #12 max
	mes: .asciiz "enter factor "
	
.text
main: 
	lw $t0, factorial
	
	add $v0, $zero, 4
	lui $1, 0x00001001
	ori $4, $1, 0x00000008
	syscall
	
	
	add $v0, $zero, 5
	syscall
	
	add $t1, $v0, $zero
	
	calc:
	blez $t1, exit
	mult  $t0, $t1
	mflo  $t0
	sub  $t1, $t1, 1
	j calc
	
	exit:
	add $v0, $zero, 1
	add $a0, $zero, $t0
	syscall