# Botoes

Botoes acionam acoes. Cada variante comunica a importancia e o tipo da acao.

> Ver exemplos visuais: [Exemplos-HTML/botoes.html](../Exemplos-HTML/botoes.html)

---

## Indice

1. [Variantes](#1-variantes)
2. [Tamanhos](#2-tamanhos)
3. [Estados](#3-estados)
4. [Icones em Botoes](#4-icones-em-botoes)
5. [Hierarquia de Acoes](#5-hierarquia-de-acoes)
6. [Regras de Uso](#6-regras-de-uso)

---

## 1. Variantes

### Primary — Acao Principal

- **Fundo:** Primary `#184194`
- **Texto:** Branco
- **Hover:** Primary Dark `#0F2D6B`
- **Uso:** A acao mais importante da tela. Ex: "Salvar", "Confirmar", "Processar"
- **Limite:** Apenas **um** botao Primary por area de acao (nao coloque dois botoes primarios lado a lado)

### Secondary — Acao Secundaria

- **Fundo:** Secondary `#6C757D`
- **Texto:** Branco
- **Hover:** `#545B62`
- **Uso:** Acoes de suporte ou alternativas. Ex: "Cancelar", "Voltar", "Fechar"

### Success — Acao Positiva

- **Fundo:** Success `#28A745`
- **Texto:** Branco
- **Hover:** `#218838`
- **Uso:** Confirmar algo que gera um resultado positivo. Ex: "Aprovar", "Ativar", "Liberar"

### Danger — Acao Destrutiva

- **Fundo:** Danger `#DC3545`
- **Texto:** Branco
- **Hover:** `#C82333`
- **Uso:** Acoes irreversiveis ou destrutivas. Ex: "Excluir", "Inativar", "Remover"
- **Obrigatorio:** Sempre exigir confirmacao antes de executar acao Danger

### Warning — Acao de Atencao

- **Fundo:** Warning `#FFC107`
- **Texto:** Escuro `#212529`
- **Hover:** `#E0A800`
- **Uso:** Acoes que exigem atencao do usuario. Ex: "Reprocessar", "Forcar Envio"

### Info — Acao Informativa

- **Fundo:** Info `#17A2B8`
- **Texto:** Branco
- **Hover:** `#138496`
- **Uso:** Acoes de consulta ou informacao. Ex: "Ver Detalhes", "Historico"

### Ghost / Outline — Acao Sutil

- **Fundo:** Transparente
- **Borda:** Primary `#184194`
- **Texto:** Primary `#184194`
- **Hover:** Fundo levemente azulado
- **Uso:** Acoes menos importantes que nao devem competir visualmente com o Primary

### Icon Button — Botao de Icone

- **Fundo:** Transparente (hover: `#F5F7FF`)
- **Conteudo:** Apenas icone (14-16px)
- **Borda:** Nenhuma
- **Uso:** Acoes em linhas de tabela (Editar, Excluir, Visualizar)
- **Obrigatorio:** Sempre com tooltip descrevendo a acao

---

## 2. Tamanhos

| Tamanho | Altura | Padding | Fonte | Uso |
|---------|--------|---------|-------|-----|
| **Small** | 28px | 10px H, 3px V | 12px | Dentro de linhas de tabela, areas compactas |
| **Medium** | 34px | 16px H, 8px V | 14px | **Padrao** — use na maioria dos casos |
| **Large** | 40px | 24px H, 12px V | 14-16px | Chamada para acao principal na pagina |

> **Regra de alinhamento:** Botoes e inputs na mesma linha devem ter a mesma altura. Use Medium (34px) para ambos como padrao.

---

## 3. Estados

| Estado | Visual | Comportamento |
|--------|--------|---------------|
| **Normal** | Cor principal da variante | Interativo |
| **Hover** | Cor mais escura | Mouse em cima |
| **Pressed / Active** | Ainda mais escuro | Click/tap |
| **Disabled** | Cinza `#94A3B8`, texto `#CBD5E1` | Nao interativo, nao recebe focus |
| **Loading** | Spinner + texto "Aguarde..." | Operacao em andamento — bloqueado |

### Botao Disabled

- Nunca oculte um botao desabilitado — mantenha visivel para o usuario entender que a acao existe
- Se possivel, adicione um tooltip explicando por que esta desabilitado

### Botao Loading

- Exiba um indicador visual (spinner ou animacao) enquanto a operacao esta em andamento
- Bloquear cliques multiplos e fundamental — use o estado loading para isso

---

## 4. Icones em Botoes

### Icone + Texto (recomendado)

O icone fica a esquerda do texto, com 6px de espaco entre eles.

```
[ + Novo Funcionario ]
[ Salvar ]  (sem icone tambem e valido quando o texto e claro)
[ Excluir ]
```

### So Icone (permitido com restricoes)

Use apenas quando:
- O espaco e muito limitado (linhas de tabela)
- O significado do icone e obvio (X para fechar, lupa para buscar)
- Um tooltip obrigatorio esta presente

---

## 5. Hierarquia de Acoes

Em qualquer barra de acoes, siga esta ordem de esquerda para direita ou de maior para menor destaque:

```
[Acao Principal - Primary]   [Acao Secundaria]   [Cancelar/Voltar]
```

**Exemplos praticos:**

```
Formulario de cadastro:
[ Salvar - Primary ]  [ Cancelar - Secondary ]

Confirmacao de exclusao:
[ Confirmar Exclusao - Danger ]  [ Cancelar - Secondary ]

Lista com multiplas acoes:
[ Novo - Primary ]  [ Exportar - Info ]  [ Importar - Ghost ]
```

---

## 6. Regras de Uso

### Obrigatorio

- Botoes Danger devem ter confirmacao (modal ou dialog)
- Botoes de icone devem ter tooltip
- Apenas 1 botao Primary por grupo de acoes
- Texto de botao e sempre um verbo de acao no imperativo: "Salvar", "Excluir", "Exportar"

### Evite

- Botoes com texto muito longo (mais de 3 palavras geralmente e excessivo)
- Dois botoes Primary na mesma linha
- Botao Primary para acao destrutiva — use Danger
- Botao sem texto E sem tooltip
- Desabilitar botoes sem indicar ao usuario por que estao desabilitados

---

*Proximo: [02-Badges-Status.md](02-Badges-Status.md)*
