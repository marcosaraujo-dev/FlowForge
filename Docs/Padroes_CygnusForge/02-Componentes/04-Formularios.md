# Formularios e Campos de Entrada

Formularios sao o principal meio de coleta de dados nos sistemas da empresa. A consistencia na apresentacao de campos reduz erros e aumenta a velocidade de preenchimento.

> Ver exemplos visuais: [Exemplos-HTML/formularios.html](../Exemplos-HTML/formularios.html)
> Implementacao tecnica: [WPF/](../WPF/) | [React/](../React/)

---

## Indice

1. [Anatomia de um Campo](#1-anatomia-de-um-campo)
2. [Tipos de Campo](#2-tipos-de-campo)
3. [Estados dos Campos](#3-estados-dos-campos)
4. [Layout de Formularios](#4-layout-de-formularios)
5. [Validacao e Mensagens de Erro](#5-validacao-e-mensagens-de-erro)
6. [Campos Especiais](#6-campos-especiais)
7. [Regras de Uso](#7-regras-de-uso)

---

## 1. Anatomia de um Campo

Todo campo de formulario segue esta estrutura:

```
[ Label do Campo ]           <- Obrigatorio, sempre visivel
[ ________________________ ] <- Campo de entrada (input)
[ Texto de ajuda opcional ]  <- Pequeno, cinza claro
[ Mensagem de erro ]         <- So aparece quando ha erro
```

**Especificacoes:**
- Label: estilo `FieldLabel` — 13px SemiBold, margem inferior 5-6px
- Input: altura 34px, padding 8px horizontal / 6px vertical, borda `#DEE2E6`, border-radius 4-6px
- Texto de ajuda: 12px, cor `#ADB5BD`
- Mensagem de erro: 12px, cor `#DC3545`

**Campo obrigatorio:** Indicado com asterisco `*` apos o label — ex: "Nome Completo *"

---

## 2. Tipos de Campo

### Campo de Texto (Input)

Para entrada livre de texto: nomes, descricoes, observacoes.

- Altura: 34px (padrao) ou 28px (compacto)
- Para textos longos: area de texto multi-linha (altura auto, min 80px)

### Selecao (Combobox / Select / Dropdown)

Para escolher uma opcao de uma lista predefinida.

- Mesma altura dos inputs (34px) para alinhamento
- Deve ter um placeholder inicial: "Selecione..."
- A lista de opcoes deve ser ordenada de forma logica (alfabetica ou por relevancia)

### Calendario (Date Picker)

Para selecao de datas. Nunca usar campo de texto livre para datas — use sempre um seletor visual.

- Formato exibido: DD/MM/AAAA
- O botao de calendario abre um popover com o mes atual

### Checkbox

Para opcoes booleanas (sim/nao, ativo/inativo) ou multipla selecao de uma lista.

- Label sempre a direita do checkbox
- Margem entre checkbox e label: 8px
- Para grupos de checkbox, usar espacamento de 8px entre itens

### Radio Button

Para escolha exclusiva de uma entre poucas opcoes (2-5 opcoes).

- Se houver mais de 5 opcoes, prefira Combobox
- Labels sempre visiveis, sem "Outros" generico

---

## 3. Estados dos Campos

| Estado | Visual | Quando ocorre |
|--------|--------|---------------|
| **Normal** | Borda `#DEE2E6`, fundo branco | Estado padrao |
| **Foco** | Borda azul `#93C5FD`, levemente mais espessa | Campo ativo, sendo editado |
| **Hover** | Borda `#CBD5E1` | Mouse em cima do campo |
| **Preenchido** | Borda normal, texto `#333333` | Campo com valor |
| **Erro** | Borda `#DC3545` + mensagem de erro abaixo | Valor invalido ou campo obrigatorio vazio |
| **Desabilitado** | Fundo `#F5F5F5`, borda `#E9ECEF`, texto `#94A3B8` | Campo nao editavel |
| **Somente Leitura** | Visual discreto, sem borda de foco | Informacao exibida mas nao editavel |

---

## 4. Layout de Formularios

### 1 Coluna — Formularios simples

Use para formularios com poucos campos ou telas estreitas.

```
[ Label ]
[ Campo ]

[ Label ]
[ Campo ]

[ Label ]
[ Campo ]
```

### 2 Colunas — Formularios padrao

O layout mais comum. Campos relacionados ficam lado a lado.

```
[ Label ]         [ Label ]
[ Campo ]         [ Campo ]

[ Label ]         [ Label ]
[ Campo ]         [ Campo ]
```

**Regra:** Campos do mesmo contexto (Nome + Sobrenome, CEP + Cidade) ficam na mesma linha.

### 3 Colunas — Formularios densos

Para telas de entrada massiva de dados onde o espaco e critico.

**Cuidado:** Nao use 3 colunas em telas estreitas. Em dispositivos ou janelas menores, as colunas devem colapsar para 1 ou 2.

### Campo que ocupa a linha inteira

Alguns campos sempre devem ocupar 100% da largura:
- Campos de observacao / texto livre longo
- Campos de busca (search)
- Campos com muitas opcoes no dropdown

---

## 5. Validacao e Mensagens de Erro

### Quando validar

- **Ao sair do campo (blur):** Para campos com formato especifico (CPF, email, data)
- **Ao tentar salvar:** Para campos obrigatorios nao preenchidos
- **Em tempo real:** Apenas quando o feedback imediato e vantajoso (ex: forca de senha)

### Mensagem de erro

- Aparece logo abaixo do campo, em vermelho
- Deve ser especifica: "CPF invalido" — nao "Dado invalido"
- O campo recebe borda vermelha simultaneamente
- Desaparece quando o valor e corrigido

### Exemplo de mensagens corretas

| Campo | Erro | Mensagem boa | Mensagem ruim |
|-------|------|-------------|---------------|
| CPF | Formato invalido | "CPF deve estar no formato 000.000.000-00" | "Campo invalido" |
| Data | Fora do periodo | "A data deve estar entre 01/2024 e 12/2024" | "Data invalida" |
| Nome | Vazio | "Nome e obrigatorio" | "Campo obrigatorio" |
| Email | Formato errado | "Informe um email valido (ex: nome@empresa.com)" | "Email invalido" |

---

## 6. Campos Especiais

### Campo de Busca com Lookup

Combina um campo de texto com uma lista de resultados dinamicos. O usuario digita e ve sugestoes em tempo real.

- Exibe pelo menos 2 campos de identificacao do resultado (ex: Codigo + Nome)
- Permite limpar a selecao
- Mostra estado de carregamento durante a busca

### Campo de Competencia (Mes/Ano)

Campo especifico para selecao de competencia fiscal (MM/AAAA).

- Dois campos numericos: Mes (1-12) + Ano (4 digitos)
- Ou um campo unico com mascara MM/AAAA

### Campo de Valor Monetario

- Alinhado a direita
- Mascara com separador de milhar e 2 casas decimais
- Formato: R$ 1.234,56

---

## 7. Regras de Uso

### Obrigatorio

- Todo campo deve ter label visivel — nunca use apenas placeholder como substituto ao label
- Campos obrigatorios sao marcados com `*`
- O asterisco e explicado alguma vez na pagina ("* Campos obrigatorios")
- Validacao de erro e especifica e no campo onde ocorreu

### Evite

- Formularios com mais de 15 campos sem divisao em secoes ou abas
- Campos que so aparecem apos outro campo ser preenchido (campos condicionais) sem indicacao visual clara
- Placeholder como unica orientacao de formato — use tambem texto de ajuda permanente
- Desabilitar o botao de salvar enquanto ha erros — e melhor mostrar os erros ao clicar

---

*Anterior: [03-Cards.md](03-Cards.md) | Proximo: [05-Tabelas.md](05-Tabelas.md)*
