# Tabelas e Grades de Dados

Tabelas sao o componente mais usado para exibir colecoes de registros. Consistencia no comportamento das linhas e fundamental para boa experiencia.

> Ver exemplos visuais: [Exemplos-HTML/tabelas.html](../Exemplos-HTML/tabelas.html)
> Implementacao tecnica: [WPF/](../WPF/) | [React/](../React/)

---

## Indice

1. [Anatomia de uma Tabela](#1-anatomia-de-uma-tabela)
2. [Estados de Linha](#2-estados-de-linha)
3. [Colunas e Larguras](#3-colunas-e-larguras)
4. [Coluna de Acoes](#4-coluna-de-acoes)
5. [Cabecalho de Coluna](#5-cabecalho-de-coluna)
6. [Estado Vazio](#6-estado-vazio)
7. [Paginacao](#7-paginacao)
8. [Regras de Uso](#8-regras-de-uso)

---

## 1. Anatomia de uma Tabela

```
+----------------------------------------------------------+
| [ Coluna A ]   [ Coluna B ]   [ Coluna C ]   [ Acoes ]  |  <- Cabecalho (header)
|----------------------------------------------------------|
| Valor          Valor          Valor          [ ] [ ]     |  <- Linha 1 (normal)
|----------------------------------------------------------|  <- Divisor sutil
| Valor          Valor          Valor          [ ] [ ]     |  <- Linha 2 (hover)
|----------------------------------------------------------|
| Valor          Valor          Valor          [ ] [ ]     |  <- Linha 3 (selecionada)
+----------------------------------------------------------+
```

**Especificacoes:**
- **Cabecalho:** fundo `#F8F9FA`, altura 40px, texto 11-12px SemiBold uppercase, borda inferior 2px `#DEE2E6`
- **Linha:** altura 48px, padding lateral 16px, divisor inferior 1px `#E9ECEF`
- **Fundo:** branco (linhas alternadas sao opcionais e devem ser muito sutis)

---

## 2. Estados de Linha

Esta e uma das especificacoes mais importantes — padroniza a interacao em todas as tabelas.

| Estado | Cor de Fundo | Hex | Quando ocorre |
|--------|-------------|-----|---------------|
| **Normal** | Branco | `#FFFFFF` | Estado padrao |
| **Hover** | Azul muito claro | `#F5F8FF` | Mouse em cima da linha |
| **Selecionada** | Azul claro | `#E3EFFF` | Linha clicada/selecionada |
| **Hover + Selecionada** | Azul medio | `#D6E8FF` | Mouse em cima de linha selecionada |

**Por que estas cores?** O tom azulado cria uma conexao visual com a cor primaria sem ser agressivo. A progressao de tons comunica clareza sobre qual linha esta ativa.

---

## 3. Colunas e Larguras

### Estrategias de largura

| Estrategia | Quando usar |
|-----------|-------------|
| **Fixo** | Colunas com conteudo de tamanho previsivel: ID (60px), Status (100px), Data (110px), Valor (120px) |
| **Proporcional (%)** | Colunas de texto variavel: Nome, Descricao, Observacao |
| **Auto** | Coluna de acoes — se ajusta ao numero de botoes |

### Alinhamento

| Tipo de dado | Alinhamento |
|-------------|-------------|
| Texto / Nomes | Esquerda |
| Numeros / Valores | Direita |
| Status / Badges | Centro |
| Acoes | Centro ou Direita |
| Datas | Esquerda ou Centro |

### Texto truncado

Para colunas com largura fixa e texto longo:
- Truncar com reticencias `...` ao final
- Mostrar o texto completo em tooltip ao passar o mouse

---

## 4. Coluna de Acoes

A ultima coluna da tabela contem os botoes de acao por linha.

### Estrutura

```
| [ Editar ] [ Visualizar ] [ Excluir ] |
```

- Botoes de icone (Icon Button): 32x32px, sem texto
- Ordenados do menos destrutivo para o mais destrutivo: Visualizar → Editar → Excluir
- Cada botao tem tooltip obrigatorio

### Limite de botoes

- Maximo **3-4 icones** na coluna de acoes
- Se precisar de mais acoes, use um menu de "..." (mais opcoes)

---

## 5. Cabecalho de Coluna

- Fundo levemente cinza `#F8F9FA`
- Fonte 11-12px, SemiBold ou Bold
- Texto em MAIUSCULAS para diferenciacao visual
- Cor do texto: `#6C757D` (secundario)
- Altura: 40px

### Ordenacao

Quando a coluna suporta ordenacao:
- Icone de seta ao lado do label (▲ asc / ▼ desc)
- Coluna ordenada tem destaque visual (icone visivel, coluna levemente diferente)
- Coluna nao ordenada mostra icone apenas no hover

---

## 6. Estado Vazio

Quando a tabela nao tem dados para exibir, mostre um estado vazio descritivo — nunca uma tabela vazia sem mensagem.

### Estrutura do estado vazio

```
+------------------------------------------+
|                                          |
|         [Icone grande — neutro]          |
|                                          |
|      Nenhum registro encontrado          |  <- Titulo
|   Ajuste os filtros ou adicione um       |  <- Subtitulo (opcional)
|         novo registro.                   |
|                                          |
|         [ Adicionar Novo ]               |  <- Acao (opcional)
|                                          |
+------------------------------------------+
```

- Icone: 48-64px, cor neutra
- Titulo: 16px SemiBold, cor Text Primary
- Subtitulo: 13px, cor Text Secondary
- Acao: opcional, somente se fizer sentido contextual

---

## 7. Paginacao

Para tabelas com muitos registros, use paginacao em vez de scroll infinito.

### Componentes da paginacao

```
[ < Anterior ]  [ 1 ] [ 2 ] [3] [ 4 ] [ 5 ]  [ Proximo > ]
                Exibindo 21-30 de 247 registros
```

- Pagina atual em destaque (Primary)
- Paginas adjacentes visiveis
- Texto informativo: "Exibindo X-Y de Z registros"
- Opcao de items por pagina: 10 / 25 / 50 / 100

---

## 8. Regras de Uso

### Obrigatorio

- Sempre implementar os 4 estados de linha (normal/hover/selected/hover+selected)
- Estado vazio com mensagem e icone — nunca tabela em branco
- Coluna de acoes sempre com tooltip em cada botao
- Colunas de valor monetario alinhadas a direita

### Evite

- Mais de 8-10 colunas visiveis simultaneamente (dificulta leitura)
- Colunas com largura auto que ficam muito pequenas para o conteudo
- Linha de cabecalho com multiplas linhas de texto (o header deve ter uma linha)
- Botoes de texto completo na coluna de acoes (use icones)

---

*Anterior: [04-Formularios.md](04-Formularios.md) | Proximo: [06-Alertas.md](06-Alertas.md)*
