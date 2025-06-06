global _start
_start:
    mov rdi, 1
    mov rax, 1
    mov rsi, a
    mov rdx, alen
    syscall
    mov rdi, 1
    mov rax, 1
    mov rsi, b
    mov rdx, blen
    syscall
    mov rax, 60
    mov rdi, 0
    syscall
section .data:
a: db 'Hello world', 10
alen: equ $-a
b: db '12', 10
blen: equ $-b
