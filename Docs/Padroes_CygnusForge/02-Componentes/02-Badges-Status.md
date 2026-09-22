# Badges e Indicadores de Status

Badges sao elementos compactos que comunicam o estado ou categoria de um registro de forma visual e imediata.

> Ver exemplos visuais: [Exemplos-HTML/badges.html](../Exemplos-HTML/badges.html)

---

## Indice

1. [Tipos de Badge](#1-tipos-de-badge)
2. [Status Badge — Estado do Registro](#2-status-badge---estado-do-registro)
3. [Type Badge — Categoria ou Tipo](#3-type-badge---categoria-ou-tipo)
4. [Tamanhos e Anatomia](#4-tamanhos-e-anatomia)
5. [Regras de Uso](#5-regras-de-uso)

---

## 1. Tipos de Badge

Existem dois tipos distintos de badge no Design System:

| Tipo | Proposito | Exemplo |
|------|-----------|---------|
| **Status Badge** | Indica o estado atual de um registro | Ativo, Inativo, Pendente, Em Analise |
| **Type Badge** | Indica a categoria ou tipo de um item | Folha, Ferias, Rescisao, SPED |

---

## 2. Status Badge — Estado do Registro

Usa as cores semanticas para comunicar o estado.

### Variantes de Status

| Variante | Fundo | Texto | Significado | Exemplos de uso |
|----------|-------|-------|-------------|-----------------|
| **Success** | `#D4EDDA` | `#155724` | Ativo, Concluido, Aprovado | Status de funcionario ativo |
| **Danger / Error** | `#F8D7DA` | `#721C24` | Inativo, Erro, Reprovado | Funcionario inativo, divergencia |
| **Warning** | `#FFF3CD` | `#856404` | Pendente, Atencao | Aguardando aprovacao |
| **Info** | `#D1ECF1` | `#0C5460` | Em Analise, Em Processamento | Processando importacao |
| **Secondary** | `#E2E3E5` | `#383D41` | Rascunho, Inativo, Archivado | Template desativado |

### Quando usar cada variante

```
Funcionario Ativo          --> Success (verde)
Funcionario Inativo        --> Danger (vermelho)
Ferias Agendadas           --> Info (azul claro)
Pagamento Pendente         --> Warning (amarelo)
Documento em Revisao       --> Info ou Warning
Calculo Enviado            --> Success
Calculo com Divergencia    --> Danger
Rascunho                   --> Secondary
```

---

## 3. Type Badge — Categoria ou Tipo

Usa cores de identidade para classificar o tipo do item, nao o seu estado.

### Variantes de Tipo

| Variante | Fundo | Texto | Uso |
|----------|-------|-------|-----|
| **Primary** | `#184194` | Branco | Tipo principal / padrao |
| **Secondary** | `#6C757D` | Branco | Tipo secundario |
| **Light** | `#F8F9FA` | `#495057` | Tipo neutro, sem destaque |

### Exemplos

```
Tipo de documento:
[Folha]    --> Primary badge
[Ferias]   --> Info badge
[Rescisao] --> Danger badge

Categoria de template:
[Mensal]   --> Primary
[Anual]    --> Secondary
```

---

## 4. Tamanhos e Anatomia

### Anatomia

```
+--[ Texto do status ]--+
|  8px  |  texto  |  8px  |    <- Padding horizontal: 8px
        3px acima e abaixo     <- Padding vertical: 3px
```

### Especificacoes

- **Padding:** 8px horizontal, 3px vertical
- **Border Radius:** 10px (forma de pilula)
- **Fonte:** 12px, SemiBold
- **Borda:** Nenhuma (fundo colorido ja e suficiente)

---

## 5. Regras de Uso

### Obrigatorio

- Badges sao elementos de **leitura** — nunca devem ser clicaveis (use botoes para acoes)
- Use sempre a variante semanticamente correta (nao use Success para um estado negativo so porque voce prefere verde)
- Em colunas de tabela, alinhe badges verticalmente ao centro

### Limite por linha

- No maximo **2 badges** lado a lado em uma celula de tabela
- Se precisar de mais de 2 badges por item, reconsidere a informacao que esta exibindo

### Badges vs Texto simples

Use badge quando:
- O estado e um dos valores de uma lista finita (Ativo/Inativo, Pendente/Aprovado/Recusado)
- O status e uma informacao de alta visibilidade na tela

Use texto simples quando:
- E uma descricao longa ou variavel
- A informacao nao e um estado binario ou de poucos valores

---

*Anterior: [01-Botoes.md](01-Botoes.md) | Proximo: [03-Cards.md](03-Cards.md)*
