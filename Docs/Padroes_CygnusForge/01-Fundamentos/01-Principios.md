# Principios do Design System

> Antes de usar qualquer componente ou cor, entenda o "por que" por tras das decisoes de design.

---

## O que e o Design System?

Um Design System e um conjunto de regras, componentes e padroes compartilhados que guia a criacao de interfaces. Pense nele como uma linguagem comum entre designers, analistas e desenvolvedores.

**Sem Design System**, cada tela e criada do zero. Botoes ficam com tamanhos diferentes. Cores variam por modulo. O usuario precisa reaprender como a interface funciona a cada tela nova.

**Com Design System**, todos os sistemas da empresa falam a mesma lingua visual. Um usuario que aprendeu a usar o modulo de Folha ja sabe como usar o modulo de Ponto, porque os padroes sao os mesmos.

---

## Os 6 Principios

### 1. Consistencia

> "Coisas iguais devem parecer iguais."

Um botao de salvar sempre tem a mesma cor, tamanho e posicao — independentemente do modulo. Um campo obrigatorio sempre e sinalizado da mesma forma. Isso reduz o esforco cognitivo do usuario.

**Na pratica:**
- Use sempre os componentes do Design System, nunca crie variantes novas sem necessidade
- Se voce precisar de algo que nao existe no DS, documente e discuta com o time antes de criar

---

### 2. Clareza

> "O usuario deve entender o que fazer sem precisar pensar."

Cada elemento da tela deve ter um proposito obvio. Titulos deixam claro o contexto. Botoes descrevem a acao. Mensagens de erro explicam o que aconteceu e o que fazer.

**Na pratica:**
- Textos de botoes sao verbos de acao: "Salvar", "Cancelar", "Exportar" — nunca "OK" ou "Continuar" sem contexto
- Campos de formulario sempre tem label visivel
- Mensagens de erro sao especificas: "CPF invalido" em vez de "Dado invalido"

---

### 3. Hierarquia Visual

> "O olho deve saber o que e mais importante sem esforco."

Tamanho, cor e peso da fonte criam uma ordem de leitura natural. O titulo da pagina e maior. A acao principal e o botao mais destacado. Informacoes secundarias ficam em cinza mais claro.

**Na pratica:**
- Siga a escala tipografica — nao invente tamanhos intermediarios
- Uma pagina deve ter apenas um elemento de destaque maximo (titulo, botao primario)
- Nao coloque tudo em negrito — negrito perde o significado se usado em excesso

---

### 4. Feedback

> "O sistema sempre deve comunicar o que esta acontecendo."

Quando o usuario realiza uma acao, o sistema responde. Clicou em salvar? Aparece loading, depois sucesso ou erro. Enviou um formulario com campos invalidos? Os campos errados ficam destacados imediatamente.

**Na pratica:**
- Use alertas de sucesso apos acoes concluidas
- Use loading em operacoes que demorem mais de 300ms
- Use estados de erro nos campos com mensagem explicativa
- Nunca deixe o usuario sem resposta apos uma acao

---

### 5. Economia Visual

> "Menos e mais — cada elemento deve ter uma razao para existir."

Excesso de botoes, cores, icones e textos cria ruido visual e dificulta a compreensao. A interface deve mostrar apenas o que e necessario para a tarefa atual.

**Na pratica:**
- Nao repita informacoes que o usuario ja sabe (ex: nao coloque "Nome do Funcionario:" antes do nome se ja esta em uma coluna com header "Nome")
- Icones sem texto so sao aceitaveis quando o significado e universalmente obvio (ex: lupa para busca)
- Remova campos, botoes e secoes que nao sao usados

---

### 6. Acessibilidade

> "A interface deve funcionar para todos os usuarios, incluindo os que tem dificuldades visuais ou motoras."

Contraste adequado entre texto e fundo, tamanhos de fonte legiveis, suporte a teclado e navegacao clara sao requisitos, nao opcoes.

**Na pratica:**
- Nunca use apenas cor para transmitir informacao (ex: nao use so vermelho para indicar erro — use tambem um icone ou texto)
- Mantenha contraste minimo de 4.5:1 para texto normal (a paleta de cores do DS foi definida com isso em mente)
- Mantenha tamanho minimo de fonte de 12px para texto legivel
- O sistema suporta escala de acessibilidade (Normal / A / A+) via F2

---

## Os quatro niveis de decisao

Ao criar uma tela, pense nesta ordem:

```
1. TOKENS        Qual cor, fonte e espacamento usar?
                 -> Resposta: Design Tokens (ver Cores, Tipografia, Espacamentos)

2. COMPONENTE    Que elemento usar para esta necessidade?
                 -> Resposta: Biblioteca de componentes (ver Botoes, Cards, Tabelas...)

3. PADRAO        Como organizar os componentes na tela?
                 -> Resposta: Padroes de layout (ver Estrutura de Paginas)

4. ESPECIFICO    Esta tela tem alguma necessidade unica?
                 -> Resposta: Documente o caso especial e discuta com o time
```

---

## O que este Design System NAO e

- Nao e uma lista de regras rigidas que nao podem ser adaptadas — e uma base que pode evoluir
- Nao e exclusivo para um framework — os principios e tokens se aplicam a WPF, React e qualquer outro
- Nao substitui o julgamento do desenvolvedor ou analista — use o bom senso quando necessario
- Nao e estatico — deve ser atualizado quando novos padroes forem identificados

---

*Proximo: [02-Cores.md](02-Cores.md) — Sistema de cores completo*
