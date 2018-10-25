#language: pt-br
Funcionalidade: Login
	Como usuário
	Eu quero ser autenticado
	Para que eu possa acessar o sistema

Contexto: Acessar o sistema Mantis
	Dado que eu acesso a tela 'Mantis'

Esquema do Cenário: Login Válido
	Quando eu realizo o login com os dados
	| dado  | valor   |
	| login | <login> |
	| senha | <senha> |
	Então o usuário deve ser autenticado com sucesso 

Exemplos:
	| login | senha |
	|       |       |

Cenário: Login Inválido
	Quando eu realizo o login com os dados
	| dado  | valor   |
	| login | <login> |
	| senha | <senha> |
	Então deve ser apresentado a mensagem ''