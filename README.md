# api-herois

# Resumo e Explicações
    - Essa é a minha api para o back-end do desafio técnico.

    - Eu não consegui um requesito secundário do teste, que era ter uma tabela de superpoderes e uma tabela de relação entre  superpodres e heróis. Porém preferi fazer algo que eu sei e evitar usar ferramentas externas, como stackoverflow e chatGPT.

    - Mas, todos os outros requisitos foram cumpridos.

# API 
    -Com essa API, é possível:
        - Cadastrar um herói
        - Excluir um Herói pelo ID
        - Atualizar um Herói
        - Listar Heróis
        - Filtrar Herói pelo ID

# Banco de Dados
    - O banco de dados foi desenvolvido com MySQL (então é necessário ter o MySQL instalado na máquina).
    - A query para a criação da tabela está no arquivo "queryParaCriarTabelaNoBanco.sql".
    - Não será necessário usar a query para criar as tabelas pois elas serão criadas com as migrations.

# Rodar API localmente
    1 - Para iniciar a API é necessário clonar o projeto com comando "git clone https://github.com/pedrolteixeira/api-herois.git"
    2 - Criar um novo database no seu MySQL chamado "apiheroi_teste"
    3 - dotnet ef database update (comando para criar a tabela no banco de dados)
    4 - dotnet run
    5 - Pronto, agora a api está rodando na URL https://localhost:5001 (O front-end do desafio que está em (https://github.com/pedrolteixeira/front-herois) usará essa url local para chamar essa API, lembrando que a api e o front tem que estár rodando simultaneamente em sua máquina).

# Swagger
    - Depois de estar rodando a API, é possível acessar a documentação via Swagger, é só entrar no endereço: https://localhost:5001/swagger/index.html

