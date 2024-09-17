<h1>EbookStore</h1>

<p>Este projeto é uma aplicação ASP.NET Core MVC para gerenciar uma loja de ebooks. Ele foi desenvolvido com o intuito de aprender e aplicar boas práticas de desenvolvimento, incluindo o uso de FluentValidation para validação de modelos.</p>
<h2>Vídeo de Demonstração</h2>

[![Assista à demonstração no YouTube](https://img.youtube.com/vi/L3T6LevsTtU/0.jpg)](https://youtu.be/L3T6LevsTtU)

<h2>Funcionalidades</h2>
<ul>
    <li><strong>CRUD de Livros</strong>: Criação, leitura, atualização e exclusão de livros.</li>
    <li><strong>CRUD de Autores</strong>: Gerenciamento de autores, incluindo suas biografias.</li>
    <li><strong>Validação</strong>: Validação de dados de entrada utilizando FluentValidation.</li>
    <li><strong>Interface de Usuário</strong>: Interface amigável para cadastrar e editar informações.</li>
</ul>

<h2>Tecnologias Utilizadas</h2>
<ul>
    <li><strong>ASP.NET Core MVC</strong>: Framework principal da aplicação.</li>
    <li><strong>Entity Framework Core</strong>: Para mapeamento objeto-relacional (ORM).</li>
    <li><strong>FluentValidation</strong>: Para validação de modelos.</li>
    <li><strong>Bootstrap</strong>: Para estilização da interface de usuário.</li>
</ul>

<h2>Estrutura do Projeto</h2>
<ul>
    <li><strong>Models</strong>: Contém as classes de modelo, como <code>Livro</code> e <code>Autor</code>.</li>
    <li><strong>Controllers</strong>: Contém os controladores que lidam com as requisições HTTP.</li>
    <li><strong>Views</strong>: Contém as views que são renderizadas para o usuário.</li>
    <li><strong>Validation</strong>: Contém as classes de validação, como <code>LivroValidator</code>.</li>
</ul>

<h2>Como Executar o Projeto</h2>
<ol>
    <li>Clone o repositório:
        <pre><code>git clone https://github.com/seu-usuario/EbookStore.git</code></pre>
    </li>
    <li>Navegue até a pasta do projeto:
        <pre><code>cd EbookStore</code></pre>
    </li>
    <li>Restaure as dependências:
        <pre><code>dotnet restore</code></pre>
    </li>
    <li>Configure a string de conexão no <code>appsettings.json</code>.</li>
    <li>Aplique as migrações do banco de dados:
        <pre><code>dotnet ef database update</code></pre>
    </li>
    <li>Execute a aplicação:
        <pre><code>dotnet run</code></pre>
    </li>
</ol>

<h2>Contribuições</h2>
<p>Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.</p>
