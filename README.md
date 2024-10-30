# SprintAgroClimate

Optamos por adotar a arquitetura monolítica para nosso projeto devido à sua simplicidade e integração inicial. Com essa abordagem, todas as funcionalidades estão agrupadas em um único código-base, o que pode facilitar o desenvolvimento e a implementação de forma mais coesa. A arquitetura monolítica é muitas vezes mais fácil de configurar inicialmente, pois permite uma integração direta entre todos os componentes do sistema.

A principal diferença entre a arquitetura monolítica e a arquitetura de microserviços está na estrutura do sistema. Na arquitetura monolítica, todas as funcionalidades estão integradas em um único código, o que simplifica o desenvolvimento e a coordenação entre diferentes partes do sistema no início. No entanto, conforme o sistema cresce, a manutenção e a escalabilidade podem se tornar mais desafiadoras. Em contraste, a arquitetura de microserviços divide o sistema em serviços independentes, cada um responsável por uma funcionalidade específica, proporcionando maior flexibilidade e escalabilidade.

A arquitetura monolítica, por outro lado, mantém tudo em um único código, o que pode ser vantajoso para equipes pequenas ou projetos com um escopo bem definido. No entanto, uma vez que o sistema se torna mais complexo, pode haver a necessidade de refatoração ou adaptação significativa para lidar com a escalabilidade e a manutenção, já que qualquer alteração ou atualização requer uma recompilação e implantação de todo o sistema.

Optamos por adotar a arquitetura monolítica para nosso projeto devido à sua simplicidade e integração inicial. Com essa abordagem, todas as funcionalidades estão agrupadas em um único código-base, o que pode facilitar o desenvolvimento e a implementação de forma mais coesa. A arquitetura monolítica é muitas vezes mais fácil de configurar inicialmente, pois permite uma integração direta entre todos os componentes do sistema.

Além disso, estamos utilizando o padrão de design Singleton dentro da arquitetura monolítica para gerenciar o acesso a recursos compartilhados e garantir que uma única instância de certos serviços ou componentes seja criada e utilizada em toda a aplicação. O Singleton é útil para controlar o acesso a objetos que possuem estado compartilhado ou que são dispendiosos para criar e manter, como conexões de banco de dados ou configuradores de aplicações. Isso ajuda a evitar a duplicação de recursos e garante que a aplicação tenha uma única fonte de verdade para esses componentes críticos.

A principal diferença entre a arquitetura monolítica e a arquitetura de microserviços está na estrutura do sistema. Na arquitetura monolítica, todas as funcionalidades estão integradas em um único código, o que simplifica o desenvolvimento e a coordenação entre diferentes partes do sistema no início. No entanto, conforme o sistema cresce, a manutenção e a escalabilidade podem se tornar mais desafiadoras. Em contraste, a arquitetura de microserviços divide o sistema em serviços independentes, cada um responsável por uma funcionalidade específica, proporcionando maior flexibilidade e escalabilidade.

A arquitetura monolítica, por outro lado, mantém tudo em um único código, o que pode ser vantajoso para equipes pequenas ou projetos com um escopo bem definido. No entanto, uma vez que o sistema se torna mais complexo, pode haver a necessidade de refatoração ou adaptação significativa para lidar com a escalabilidade e a manutenção, já que qualquer alteração ou atualização requer uma recompilação e implantação de todo o sistema. Nesse contexto, o uso de padrões como o Singleton pode ajudar a gerenciar melhor os recursos compartilhados, mas também pode exigir cuidados adicionais para evitar problemas de acoplamento excessivo e tornar a manutenção do sistema mais complexa a longo prazo.

O Aluno Felipe Batista é da turma 2TDSPY Enquanto os demais são do 2TDSPV.

RM551191 - Diego Mascarenhas Santos

RM98482 - Sarah Oliveira Souza Rosa

RM97798 - Ester Dutra da Silva

RM550981- Henrique Gerson Costa Correia

RM99985 - Felipe Batista Gregório (2TDSPY)
