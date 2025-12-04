

Verbos HTTP, Enpoints, Status Codes

VERBO POST
ENDPOINT /produtos
STATUS CODE 201 Created
(Criar um produto)


VERBO GET
ENDPOINT /produtos/{id:long}
STATUS CODES	200 OK, 404 Not Found
(Obter um produto pelo Id)

VERBO GET
ENDPOINT /produtos
STATUS CODES	200 OK
(Obter todos os produtos)

PUT
ENDPOINT /produtos/{id:long}
STATUS CODES 200 OK, 404 Not Found, 400 Bad Request
(Atualizar um produto pelo id)

DELETE
ENDPOINT /produtos/{id:long}
STATUS CODES 204 No Content (200 Ok)
(Deletar um produto pelo id)



Métodos para lidar com Requisições HTTP:

- Ok( ) Retorna o código de status 200
- BadRequest()
- NotFound()
- CreateAtAction( )
- PhysicalFile()