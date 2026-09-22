# Catalogo de Icones

Os sistemas da empresa usam a familia de icones **Segoe MDL2 Assets** — fonte de icones nativa do Windows, disponivel em todas as versoes modernas do SO.

> Ver galeria visual: [Exemplos-HTML/icones.html](../Exemplos-HTML/icones.html)

---

## Indice

1. [Como os icones funcionam](#1-como-os-icones-funcionam)
2. [Mapeamento Rapido — Acoes Comuns](#2-mapeamento-rapido---acoes-comuns)
3. [Icones de Navegacao](#3-icones-de-navegacao)
4. [Icones de Status e Feedback](#4-icones-de-status-e-feedback)
5. [Icones de Documentos e Arquivos](#5-icones-de-documentos-e-arquivos)
6. [Icones de Interface](#6-icones-de-interface)
7. [Tamanhos Recomendados](#7-tamanhos-recomendados)
8. [Regras de Uso](#8-regras-de-uso)

---

## 1. Como os icones funcionam

A fonte Segoe MDL2 Assets funciona como uma fonte de texto — cada caractere Unicode corresponde a um icone. Para exibir o icone de "Editar", voce usa o caractere `&#xE70F;` na fonte correta.

**Em WPF:** Use TextBlock com `FontFamily="Segoe MDL2 Assets"` e o codigo no atributo `Text`.
**Em React/Web:** Nao usar a fonte Segoe MDL2 Assets (nao e cross-platform). O padrao obrigatorio para projetos React e **`@tabler/icons-react`** — ver [React/00-Stack.md](../React/00-Stack.md). O mapeamento de acoes desta pagina (Adicionar, Editar, Excluir...) vale como referencia conceitual; escolha o icone Tabler equivalente pelo nome da acao, nao pelo codigo Unicode.

> Nota: Segoe MDL2 Assets esta disponivel nativamente apenas no Windows — por isso e exclusiva do WPF. Web/React usa Tabler Icons para ficar cross-platform.

---

## 2. Mapeamento Rapido — Acoes Comuns

Os icones mais usados na interface. Use sempre estes para manter consistencia.

| Acao | Codigo Unicode | Nome na Fonte | Cor Recomendada |
|------|---------------|--------------|-----------------|
| **Adicionar / Novo** | `&#xE710;` | Add | Primary `#184194` |
| **Editar** | `&#xE70F;` | Edit | Primary `#184194` |
| **Excluir / Remover** | `&#xE74D;` | Delete | Danger `#DC3545` |
| **Salvar** | `&#xE74E;` | Save | Primary `#184194` |
| **Cancelar / Fechar (X)** | `&#xE711;` | Cancel | Secondary `#6C757D` |
| **Confirmar / Aprovar** | `&#xE8FB;` | Accept | Success `#28A745` |
| **Buscar / Pesquisar** | `&#xE721;` | Search | Secondary |
| **Atualizar / Recarregar** | `&#xE72E;` | Refresh | Primary |
| **Enviar** | `&#xE724;` | Mail Forward | Primary |
| **Download / Exportar** | `&#xE72C;` | Download | Primary |
| **Upload / Importar** | `&#xE74A;` | Up | Primary |
| **Copiar** | `&#xE8E5;` | Copy | Secondary |
| **Imprimir** | `&#xE768;` | Print | Primary |
| **Informacao** | `&#xE8A5;` | Info | Info `#17A2B8` |
| **Configuracoes** | `&#xE713;` | Settings | Secondary |

---

## 3. Icones de Navegacao

| Acao | Codigo Unicode | Nome |
|------|---------------|------|
| **Inicio / Home** | `&#xE72A;` | Home |
| **Voltar** | `&#xE76B;` | Back |
| **Avancar** | `&#xE76C;` | Forward |
| **Expandir (seta baixo)** | `&#xE70D;` | ChevronDown |
| **Recolher (seta cima)** | `&#xE70E;` | ChevronUp |
| **Seta direita** | `&#xE76C;` | ChevronRight |
| **Menu / Hamburguer** | `&#xE700;` | GlobalNavButton |
| **Fechar painel** | `&#xE8BB;` | ChromeClose |

---

## 4. Icones de Status e Feedback

| Estado | Codigo Unicode | Nome | Cor |
|--------|---------------|------|-----|
| **Sucesso / OK** | `&#xE73E;` | CheckMark | Success |
| **Erro / Invalido** | `&#xE783;` | ErrorBadge | Danger |
| **Aviso / Atencao** | `&#xE7BA;` | Warning | Warning |
| **Informacao** | `&#xE8A5;` | Info | Info |
| **Carregando** | `&#xE8A7;` | Sync | Secondary |
| **Bloqueado** | `&#xE72E;` | Lock (usar E72E) | Secondary |
| **Favorito** | `&#xE734;` | FavoriteStar | Warning |

---

## 5. Icones de Documentos e Arquivos

| Item | Codigo Unicode | Nome |
|------|---------------|------|
| **Documento / Arquivo** | `&#xE8A5;` | Document |
| **Pasta** | `&#xE8B7;` | Folder |
| **Word (.docx)** | `&#xE8A5;` | Document |
| **Excel / Planilha** | `&#xE9F9;` | Table (usar para planilha) |
| **PDF** | `&#xE8A5;` | Document |
| **Calendario** | `&#xE787;` | Calendar |
| **Relatorio** | `&#xE9F9;` | ReportDocument |

---

## 6. Icones de Interface

| Elemento | Codigo Unicode | Nome |
|----------|---------------|------|
| **Usuario / Pessoa** | `&#xE77B;` | People |
| **Empresa / Negocio** | `&#xE731;` | ContactInfo |
| **Filtro** | `&#xE71C;` | Filter |
| **Ordenar** | `&#xE174;` | Sort |
| **Zoom In** | `&#xE8A3;` | ZoomIn |
| **Zoom Out** | `&#xE71F;` | ZoomOut |
| **Lista** | `&#xE14C;` | BulletedList |
| **Grade** | `&#xE80A;` | GridView |
| **Olho / Visualizar** | `&#xE890;` | View |
| **Cadeado** | `&#xE72E;` | Lock |
| **Notificacao** | `&#xEA8F;` | Bell |

---

## 7. Tamanhos Recomendados

| Contexto | Tamanho | Exemplo |
|----------|---------|---------|
| Botao de acao em grid | 14px | Botoes Editar/Excluir em linhas de tabela |
| Botao padrao com texto | 14px | Botao "Novo" com icone ao lado |
| Botao de destaque | 16px | Botao grande de acao principal |
| Icone standalone | 16-20px | Icone de status ao lado de um label |
| Icone em titulo/header | 20-24px | Icone decorativo em cabecalho de pagina |

---

## 8. Regras de Uso

### Icone com texto (recomendado)

Sempre que possivel, combine icone com texto descritivo. O texto elimina ambiguidade.

```
[ + Novo Funcionario ]    <- Ideal
[ + ]                     <- Aceitavel apenas com tooltip claro
```

### Icone sem texto (quando permitido)

Botoes de icone sem texto sao aceitaveis apenas quando:
- O significado e universalmente obvio (lupa para busca, X para fechar)
- Existe um tooltip obrigatorio descrevendo a acao
- O espaco e muito limitado (botoes de acao em colunas de tabela)

### Consistencia de cor

- Icone de acao principal → cor Primary
- Icone de excluir → cor Danger
- Icone de confirmar → cor Success
- Icone de informacao → cor Info ou Secondary
- Icone decorativo → herda a cor do contexto

### O que evitar

- Usar icones sem tooltip quando o texto nao aparece
- Usar icones diferentes para a mesma acao em partes distintas do sistema
- Inventar novos icones para acoes que ja tem um icone mapeado aqui

---

*Anterior: [04-Espacamentos.md](04-Espacamentos.md) | Proximo: [../02-Componentes/01-Botoes.md](../02-Componentes/01-Botoes.md)*
