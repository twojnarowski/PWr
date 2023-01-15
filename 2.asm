.data
	factorial: .word 1 #result
	factor: .word 6   #12 max
	
.text
main: 
	lw $t0, factorial
	lw $t1, factor	

	
	calc:
	blez $t1, exit
	mul $t0, $t0, $t1
	sub  $t1, $t1, 1
	j calc
	
	exit:
	li $v0, 1
	add $a0, $zero, $t0
	syscall
