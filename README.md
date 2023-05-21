# CUF

**Cadastro Único de Fornecedores - CUF** é um sistema, que permite o cadastro de fornecedores, produtos e serviços, além de permitir a criação de pedidos de compra.

## Instalação

Para instalar o sistema é necessário ter um banco de dados MySQL ou MariaDB, não é necessário criar o banco de dados, o sistema irá criar automaticamente.
Também é necessário ter instalado na máquina o [dotnet core](https://dotnet.microsoft.com/download/dotnet/6.0) superior a versão 6.

Após clonar o repositório, entre na pasta do projeto e execute o comando abaixo para instalar suas dependências.

```bash
dotnet restore
```

## Configuração

Agora será necessário colocar as ferramentas de linha de comando do dotnet `ef` em seu `PATH`, para isso execute o comando abaixo.

```bash
dotnet tool install --global dotnet-ef
```

E em seguida execute o comando abaixo mirar as tabelas no banco de dados.

```bash
dotnet ef database update --project CUF
```

Caso esteja apenas utilizando o binario, é necessário criar um arquivo de configuração, porem lembrando,
nesse caso o sistema não irá criar o banco de dados automaticamente, então será necessário criar o banco de dados manualmente.

Um exemplo do arquivo de configuração pode ser encontrado em: [appsettings.json](/CUF/appsettings.json)

## Uso

Para executar o sistema, execute o comando abaixo.

```bash
dotnet run --project CUF
```

Para **desenvolvimento**, é recomendado utilizar o comando abaixo, que irá executar o sistema e irá atualizar automaticamente o sistema quando houver alterações no código.

```bash
dotnet watch run --project CUF
```

No sistema por padrão já existe um usuário administrador, com o login `admin@local` e senha `admin`.
Apenas o usuário administrador pode dar permissões, excluir e alterar senhas de outros usuários.

**ATENÇÃO**: O sistema não possui recuperação de senha, então caso o usuário administrador perca a senha,
será necessário alterar a senha diretamente no banco de dados. As senhas são armazenadas no banco de dados utilizando o algoritmo de hash `SHA512` sendo então serializadas em `Base64`.
Para alterar a senha do usuário administrador, execute o comando abaixo no banco de dados.

```sql
UPDATE `Users` SET `Password` = "x61Ey612Kl2gpFL56FT9weDnpSo4AV8j8+qx2AuTHdRyY036xxzTTrw10Wq3+4qQyB+XURPWx1ONxp3Y3pB37A==" WHERE `Users`.`Id` = 1;
```

Isso irá alterar a senha do usuário administrador para `admin`.