# Loading e Estados Especiais

Feedback visual durante carregamento e em situacoes sem dados e fundamental para uma boa experiencia.

---

## Indice

1. [Loading Overlay](#1-loading-overlay)
2. [Loading Inline](#2-loading-inline)
3. [Skeleton Screens](#3-skeleton-screens)
4. [Estado Vazio](#4-estado-vazio)
5. [Estado de Erro de Carregamento](#5-estado-de-erro-de-carregamento)
6. [Quando usar cada abordagem](#6-quando-usar-cada-abordagem)

---

## 1. Loading Overlay

Uma camada semitransparente que cobre a area de conteudo durante uma operacao.

### Anatomia

```
+------------------------------------------+
| (fundo semitransparente escuro ou branco) |
|                                           |
|          [Spinner girando]                |
|          "Aguardando..."                  |
|                                           |
+------------------------------------------+
```

**Especificacoes:**
- Fundo: `rgba(255,255,255,0.85)` (overlay branco) ou `rgba(0,0,0,0.3)` (overlay escuro)
- Spinner: cor Primary `#184194`, tamanho 32-40px
- Texto: 14px, cor Text Primary, opcional
- Posicao: centrado no container coberto

**Uso:** Operacoes que substituem ou atualizam o conteudo inteiro de uma area (salvar, processar calculo, carregar lista).

---

## 2. Loading Inline

Indicador de progresso dentro de um componente especifico.

### Exemplos

- **Botao loading:** o proprio botao mostra spinner + texto "Aguardando..."
- **Campo de busca:** spinner pequeno no campo enquanto busca resultados
- **Linha de tabela:** indicador discreto na linha sendo processada

**Uso:** Operacoes que afetam um elemento especifico, nao a tela inteira.

---

## 3. Skeleton Screens

Esbocos cinzas que imitam o layout do conteudo enquanto ele carrega.

```
+------------------------------------------+
| [████████████████] [██████]              |  <- Linha simulada
| [███████] [████████████████████]         |
| [██████████] [███████]                   |
+------------------------------------------+
```

**Quando usar:** Carregamento inicial de listas ou cards onde o tempo de espera pode ser maior (>1 segundo). Mais agradavel que um spinner puro porque o usuario ja ve a estrutura do conteudo.

**Especificacoes:**
- Cor: `#E9ECEF` com animacao sutil pulsante
- Altura das linhas: similar ao conteudo real
- Border radius: similar ao conteudo real

---

## 4. Estado Vazio

Quando nao ha dados para exibir (resultado de busca sem resultados, lista vazia, etc).

### Anatomia

```
+------------------------------------------+
|                                           |
|         [Icone ilustrativo 48px]          |
|                                           |
|      Nenhum registro encontrado           |  <- Titulo
|   Ajuste os filtros ou adicione um        |  <- Descricao (opcional)
|           novo registro.                  |
|                                           |
|         [ Acao principal ]                |  <- Botao (quando relevante)
|                                           |
+------------------------------------------+
```

### Tipos de estado vazio

| Situacao | Mensagem | Acao |
|----------|----------|------|
| Lista vazia (recente) | "Nenhum registro cadastrado ainda." | "Adicionar primeiro registro" |
| Busca sem resultado | "Nenhum resultado para [termo]." | "Limpar busca" |
| Filtro muito restritivo | "Nenhum item corresponde aos filtros aplicados." | "Remover filtros" |
| Sem permissao de acesso | "Voce nao tem permissao para ver estes dados." | — |
| Erro ao carregar | "Nao foi possivel carregar os dados." | "Tentar novamente" |

---

## 5. Estado de Erro de Carregamento

Quando o conteudo nao pode ser carregado por erro tecnico.

```
+------------------------------------------+
|                                           |
|          [Icone de erro 48px]             |
|                                           |
|     Nao foi possivel carregar             |  <- Titulo
|     os dados no momento.                 |
|                                           |
|    Verifique sua conexao e tente          |  <- Descricao
|    novamente.                             |
|                                           |
|       [ Tentar Novamente ]                |  <- Acao
|                                           |
+------------------------------------------+
```

---

## 6. Quando usar cada abordagem

| Duracao estimada | Abordagem recomendada |
|-----------------|----------------------|
| < 300ms | Nenhuma — resposta percebida como instantanea |
| 300ms — 1s | Loading inline ou botao loading |
| 1s — 3s | Overlay ou skeleton |
| > 3s | Overlay com mensagem descritiva + progresso se possivel |
| Desconhecido | Overlay com botao de cancelar |

---

*Anterior: [09-Tooltips.md](09-Tooltips.md) | Proximo: [11-Menu-Lateral.md](11-Menu-Lateral.md)*
