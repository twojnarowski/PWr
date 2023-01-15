.data
	a: .word 5
	b: .word 6
	c: .word 4
	d: .word 3
	
.text
main: 
	lw $t0, a
	add $a0, $zero, $t0
	lw $t0, b
	add $a0, $a0, $t0
	lw $t0, c
	sub $a0, $a0, $t0
	lw $t0, d
	add $a0, $a0, $t0
	
	li $v0, 1
	syscall