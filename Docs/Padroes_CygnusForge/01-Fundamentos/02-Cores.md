# Sistema de Cores

A paleta de cores define a identidade visual dos sistemas da empresa e comunica significado atraves da interface.

> Ver exemplos visuais: [Exemplos-HTML/cores.html](../Exemplos-HTML/cores.html)

---

## Indice

1. [Principio de Uso das Cores](#1-principio-de-uso-das-cores)
2. [Cores Primarias](#2-cores-primarias)
3. [Cores Semanticas](#3-cores-semanticas)
4. [Cores Neutras](#4-cores-neutras)
5. [Cores de Fundo](#5-cores-de-fundo)
6. [Cores de Borda](#6-cores-de-borda)
7. [Cores de Texto](#7-cores-de-texto)
8. [Cores de Componentes Especificos](#8-cores-de-componentes-especificos)
9. [Regras de Uso](#9-regras-de-uso)

---

## 1. Principio de Uso das Cores

Cada cor tem um proposito especifico. Usar a cor errada no contexto errado confunde o usuario.

| Cor | Significado | Quando usar |
|-----|------------|-------------|
| **Azul (Primary)** | Acao, identidade | Botao principal, links, destaques de navegacao |
| **Verde (Success)** | Positivo, confirmado | Salvo com sucesso, status ativo, aprovado |
| **Vermelho (Danger)** | Erro, perigo, irreversivel | Erros, excluir, acoes destrutivas |
| **Amarelo (Warning)** | Atencao, cuidado | Avisos, pendencias, acoes com consequencia |
| **Azul claro (Info)** | Informacao neutra | Dicas, informacoes complementares |
| **Cinza (Secondary)** | Secundario, neutro | Botoes de cancelar, acoes secundarias |

---

## 2. Cores Primarias

A cor principal da marca Empresa. Usada nos elementos mais importantes da interface.

| Token | Hex | Uso |
|-------|-----|-----|
| `Primary` | `#184194` | Botao primario, headers, links, destaques |
| `Primary Hover` | `#0F2D6B` | Estado hover do botao primario |
| `Primary Dark` | `#0F2D6B` | Variacao mais escura para contraste |

**Swatch:**

```
#184194  ████████████  Azul Empresa
#0F2D6B  ████████████  Azul Empresa Escuro
```

---

## 3. Cores Semanticas

Cores que comunicam estados e resultados de acoes.

### Success (Verde)

| Token | Hex | Uso |
|-------|-----|-----|
| `Success` | `#28A745` | Botao de confirmacao, status "Ativo", icone de sucesso |
| `Success Hover` | `#218838` | Hover |
| `Badge Success Background` | `#D4EDDA` | Fundo de badge "Ativo" |
| `Badge Success Text` | `#155724` | Texto de badge "Ativo" |
| `Alert Success Background` | `#D4EDDA` | Fundo de alerta de sucesso |
| `Alert Success Border` | `#28A745` | Borda de alerta de sucesso |
| `Alert Success Text` | `#155724` | Texto de alerta de sucesso |

### Danger (Vermelho)

| Token | Hex | Uso |
|-------|-----|-----|
| `Danger` | `#DC3545` | Botao excluir, status "Inativo/Erro", icone de erro |
| `Danger Hover` | `#C82333` | Hover |
| `Badge Error Background` | `#F8D7DA` | Fundo de badge "Erro" |
| `Badge Error Text` | `#721C24` | Texto de badge "Erro" |
| `Alert Danger Background` | `#F8D7DA` | Fundo de alerta de erro |
| `Alert Danger Border` | `#DC3545` | Borda de alerta de erro |
| `Alert Danger Text` | `#721C24` | Texto de alerta de erro |

### Warning (Amarelo)

| Token | Hex | Uso |
|-------|-----|-----|
| `Warning` | `#FFC107` | Botao de aviso, status "Pendente" |
| `Warning Hover` | `#E0A800` | Hover |
| `Badge Warning Background` | `#FFF3CD` | Fundo de badge "Pendente" |
| `Badge Warning Text` | `#856404` | Texto de badge "Pendente" |
| `Alert Warning Background` | `#FFF3CD` | Fundo de alerta de aviso |
| `Alert Warning Border` | `#FFC107` | Borda de alerta de aviso |
| `Alert Warning Text` | `#856404` | Texto de alerta de aviso |

> **Atencao:** Texto preto (`#212529`) sobre Warning — nao branco. O amarelo nao tem contraste suficiente com branco.

### Info (Azul Claro)

| Token | Hex | Uso |
|-------|-----|-----|
| `Info` | `#17A2B8` | Informacoes complementares, botao de detalhe |
| `Info Hover` | `#138496` | Hover |
| `Badge Info Background` | `#D1ECF1` | Fundo de badge informativo |
| `Badge Info Text` | `#0C5460` | Texto de badge informativo |
| `Alert Info Background` | `#D1ECF1` | Fundo de alerta informativo |
| `Alert Info Border` | `#17A2B8` | Borda de alerta informativo |
| `Alert Info Text` | `#0C5460` | Texto de alerta informativo |

### Secondary (Cinza)

| Token | Hex | Uso |
|-------|-----|-----|
| `Secondary` | `#6C757D` | Botao secundario, cancelar, acoes de menor prioridade |
| `Secondary Hover` | `#545B62` | Hover |

---

## 4. Cores Neutras

| Token | Hex | Uso |
|-------|-----|-----|
| `White` | `#FFFFFF` | Fundo de cards, inputs, superficies elevadas |
| `Disabled` | `#94A3B8` | Elemento desabilitado (fundo, borda) |
| `Disabled Text` | `#CBD5E1` | Texto de elemento desabilitado |

---

## 5. Cores de Fundo

| Token | Hex | Uso |
|-------|-----|-----|
| `Background` | `#F5F5F5` | Background geral da aplicacao (pagina) |
| `Sidebar Background` | `#F8F9FA` | Background do menu lateral e areas secundarias |
| `Surface` | `#FFFFFF` | Background de cards e containers elevados |
| `Hover Background` | `#E9ECEF` | Hover em itens de lista e menu |
| `Info Background` | `#D1ECF1` | Background de cards informativos |
| `DataGrid Row Hover` | `#F5F8FF` | Linha da tabela com mouse em cima |
| `DataGrid Row Selected` | `#E3EFFF` | Linha da tabela selecionada |
| `DataGrid Row Hover+Selected` | `#D6E8FF` | Linha selecionada com mouse em cima |

---

## 6. Cores de Borda

| Token | Hex | Uso |
|-------|-----|-----|
| `Border` | `#DEE2E6` | Borda padrao de cards e inputs |
| `Border Light` | `#E9ECEF` | Borda mais sutil, divisores |
| `Border Hover` | `#CBD5E1` | Borda de input em hover |
| `Border Pressed` | `#93C5FD` | Borda de input em foco/pressed |
| `Info Border Light` | `#B3D9FF` | Borda de cards informativos |

---

## 7. Cores de Texto

| Token | Hex | Uso |
|-------|-----|-----|
| `Text Primary` | `#495057` | Texto principal — corpo, labels, titulos |
| `Text Secondary` | `#6C757D` | Texto auxiliar, legendas, descricoes |
| `Text Light` | `#ADB5BD` | Texto de placeholder, ajuda, subtextos |
| `Text Field` | `#333333` | Texto dentro de inputs |
| `Link` | `#0066CC` | Links e textos clicaveis |
| `Link Hover` | `#0052A3` | Link em hover |

---

## 8. Cores de Componentes Especificos

### Dashboard Cards (acento visual)

Usadas exclusivamente em cards de navegacao do dashboard.

| Token | Hex | Contexto |
|-------|-----|---------|
| `Card Accent Documentos` | `#4A90D9` | Card de documentos |
| `Card Accent Planilhas` | `#5CB85C` | Card de planilhas |
| `Card Accent Templates` | `#5BC0DE` | Card de templates |
| `Card Accent Gerar` | `#F0AD4E` | Card de geracao |

### DataGrid Header

| Token | Hex | Uso |
|-------|-----|-----|
| `DataGrid Header Background` | `#F8F9FA` | Cabecalho da tabela |
| `DataGrid Row Border` | `#E9ECEF` | Divisor entre linhas |

---

## 9. Regras de Uso

### Obrigatorio

- Sempre use os tokens (nomes) para referenciar cores, nunca o valor hex diretamente no codigo
- Nunca invente novas cores sem aprovacao — amplie a paleta existente se necessario

### Combinacoes validas de texto/fundo

| Fundo | Texto | Contraste |
|-------|-------|-----------|
| `#184194` (Primary) | `#FFFFFF` (Branco) | Alto - OK |
| `#FFFFFF` (Branco) | `#495057` (Text Primary) | Alto - OK |
| `#F5F5F5` (Background) | `#495057` (Text Primary) | Alto - OK |
| `#FFC107` (Warning) | `#212529` (Escuro) | OK - nao usar branco |
| `#94A3B8` (Disabled) | `#CBD5E1` (Disabled Text) | Intencional - elemento inativo |

### Combinacoes invalidas

- `#FFC107` com `#FFFFFF` — contraste insuficiente
- Inventar tons intermediarios de Primary sem necessidade
- Usar cores semanticas fora do seu contexto (ex: Warning para decoracao)

---

*Anterior: [01-Principios.md](01-Principios.md) | Proximo: [03-Tipografia.md](03-Tipografia.md)*
