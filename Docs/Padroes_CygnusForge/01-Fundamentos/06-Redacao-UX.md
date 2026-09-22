# Redacao UX — Guia de Linguagem

Padrao de linguagem para todos os textos da interface dos sistemas da empresa. Aplicavel a WPF, React ou qualquer outro framework.

> **Por que isso importa:** Textos inconsistentes fazem o usuario pensar mais do que precisa. Quando o mesmo conceito tem nomes diferentes em telas diferentes, a curva de aprendizado aumenta e a confianca no sistema cai.

---

## Indice

1. [Tom e Voz](#1-tom-e-voz)
2. [Titulos de Pagina](#2-titulos-de-pagina)
3. [Labels de Campos](#3-labels-de-campos)
4. [Textos de Botoes](#4-textos-de-botoes)
5. [Mensagens do Sistema](#5-mensagens-do-sistema)
6. [Estados Vazios](#6-estados-vazios)
7. [Formatacao de Dados](#7-formatacao-de-dados)
8. [Placeholders e Textos de Ajuda](#8-placeholders-e-textos-de-ajuda)
9. [Confirmacoes e Dialogs](#9-confirmacoes-e-dialogs)
10. [Referencia Rapida](#10-referencia-rapida)

---

## 1. Tom e Voz

### O tom dos sistemas da empresa

**Profissional, direto e humano.** Os sistemas da empresa sao ferramentas de trabalho usadas por profissionais de RH, contabilidade e gestao. O tom e serio sem ser robotico, e util sem ser condescendente.

| Atributo | Significa | Exemplo |
|----------|-----------|---------|
| **Profissional** | Vocabulario do dominio (folha, competencia, rubrica) | "Competencia" — nao "mes de referencia" |
| **Direto** | Frases curtas, sem enrolacao | "Salvar" — nao "Clique aqui para salvar suas alteracoes" |
| **Humano** | Mensagens de erro que explicam, nao culpam | "Nao foi possivel salvar." — nao "Erro 500" |
| **Consistente** | Mesmo termo para o mesmo conceito em toda a aplicacao | Sempre "Funcionario" — nunca ora "funcionario", ora "colaborador", ora "empregado" |

### O que evitar

- **Jargao tecnico exposto ao usuario:** "Timeout na requisicao", "NullReferenceException"
- **Culpa ao usuario:** "Voce nao preencheu o campo obrigatorio" — prefira "Nome e obrigatorio"
- **Passivo agressivo:** "Tem certeza mesmo?" — prefira "Confirmar exclusao"
- **Redundancia:** "Clique no botao Salvar para salvar" — so "Salvar"
- **Maiusculas desnecessarias:** "Clique Aqui Para Ver o Relatorio" — so "Ver relatorio"

---

## 2. Titulos de Pagina

### Padrao

**Substantivo ou frase nominal — sem verbo no imperativo.**

| Tipo de tela | Padrao | Exemplos corretos | Exemplos errados |
|-------------|--------|-------------------|------------------|
| Lista de registros | Substantivo no plural | "Funcionarios", "Templates", "Empresas" | "Listar Funcionarios", "Ver Funcionarios" |
| Cadastro novo | "Novo(a) [Entidade]" | "Novo Template", "Nova Empresa" | "Cadastrar Template", "Adicionar Template" |
| Edicao | "Editar [Entidade]" | "Editar Funcionario", "Editar Template" | "Alterar Funcionario", "Modificar Dados" |
| Detalhe | Nome do registro | "Joao da Silva", "Acordo de Compensacao" | "Detalhe do Funcionario", "Ver Funcionario" |
| Dashboard | "[Modulo] — Resumo" ou "[Modulo]" | "Folha — Resumo", "Painel Principal" | "Home", "Dashboard", "Inicio" |
| Wizard | Descricao da tarefa | "Gerar Documentos", "Importar Funcionarios" | "Wizard de Geracao", "Passo a passo" |
| Configuracoes | "Configuracoes de [Escopo]" | "Configuracoes da Empresa", "Configuracoes Gerais" | "Setup", "Preferencias" |

### Capitalizacao

- Apenas a **primeira letra** da frase em maiusculo, exceto nomes proprios
- Nao usar Title Case em titulos (exceto se for nome proprio)

```
Correto:  "Funcionarios"
Correto:  "Joao da Silva"
Errado:   "Cadastro De Funcionarios"
Errado:   "FUNCIONARIOS"
```

### Subtitulo (opcional)

Quando a pagina tem contexto relevante (nome da empresa, competencia, status), o subtitulo aparece abaixo do titulo em `SecondaryText`.

```
Editar Funcionario
Joao da Silva — Matricula 001 — Alfa SA
```

---

## 3. Labels de Campos

### Padrao

**Substantivo ou frase nominal curta — sem "do", "de" desnecessarios.**

```
Correto:  "Nome Completo"
Correto:  "Data de Admissao"
Correto:  "Salario Base"
Errado:   "Nome Completo do Funcionario"
Errado:   "Qual e o nome completo?"
Errado:   "Nome:"  (sem dois-pontos em labels de formulario)
```

### Campos obrigatorios

Marcar com `*` imediatamente apos o label, sem espaco. O asterisco usa `DangerColor`.

```
Nome Completo *
CPF *
```

Incluir legenda em algum lugar visivel do formulario (geralmente no rodape ou no topo): `* Campos obrigatorios`

### Consistencia de terminologia

Usar sempre os mesmos termos em todos os modulos. Nao variar por tela.

| Termo correto | Nao usar |
|---------------|---------|
| Funcionario | Colaborador, Empregado, Trabalhador (exceto quando for o termo tecnico do contexto) |
| Empresa | Empregador, Organizacao |
| Competencia | Mes de referencia, Periodo, Mes/ano |
| Matricula | Codigo do funcionario, ID, Numero |
| Admissao | Contratacao, Inicio |
| Demissao | Desligamento, Saida |
| Salario base | Salario bruto inicial, Remuneracao base |
| Rubrica | Evento, Verba |
| Template | Modelo, Documento modelo |
| Coringa | Placeholder (apenas em contexto tecnico) |

---

## 4. Textos de Botoes

### Padrao

**Verbo no infinitivo + objeto quando necessario para clareza.**

| Acao | Texto correto | Nao usar |
|------|---------------|---------|
| Salvar formulario | "Salvar" ou "Salvar Alteracoes" | "OK", "Confirmar", "Enviar", "Aplicar" |
| Criar registro | "Criar [Entidade]" ou "+ Novo [Entidade]" | "Adicionar", "Inserir", "Incluir" |
| Editar registro | "Editar" | "Alterar", "Modificar", "Update" |
| Excluir registro | "Excluir" | "Deletar", "Apagar", "Remover" |
| Cancelar acao | "Cancelar" | "Voltar", "Fechar", "Nao" |
| Voltar a tela anterior | "Voltar" | "Cancelar", "Fechar" |
| Gerar documento | "Gerar" ou "Gerar Documento" | "Executar", "Processar", "Rodar" |
| Exportar | "Exportar" ou "Exportar para Excel" | "Download", "Baixar relatorio" |
| Importar | "Importar" | "Upload", "Carregar arquivo" |
| Pesquisar | "Buscar" | "Pesquisar", "Search", "Filtrar" |
| Confirmar exclusao | "Excluir" (em vermelho, no dialog) | "Sim", "Confirmar", "OK" |
| Proximo passo | "Proximo" | "Continuar", "Avancar", ">" |
| Passo anterior | "Anterior" | "Voltar", "Retroceder", "<" |
| Ultimo passo (wizard) | "Confirmar" ou "Gerar" ou "Finalizar" | "Concluir", "Done", "Submit" |

### Botoes de icone (sem texto)

Botoes com apenas icone **obrigatoriamente** tem `ToolTip` com o nome da acao.

```
Icone de lapis  → ToolTip: "Editar"
Icone de lixo   → ToolTip: "Excluir"
Icone de olho   → ToolTip: "Visualizar"
Icone de baixar → ToolTip: "Baixar documento"
```

### Capitalizacao em botoes

Apenas a primeira letra da primeira palavra em maiusculo.

```
Correto:  "Salvar alteracoes"
Correto:  "Novo funcionario"
Errado:   "Salvar Alteracoes"
Errado:   "SALVAR"
```

---

## 5. Mensagens do Sistema

### Loading

Descrever o que esta acontecendo, nao so "Carregando...".

| Situacao | Texto correto | Nao usar |
|----------|---------------|---------|
| Carregar lista | "Carregando funcionarios..." | "Carregando...", "Aguarde..." |
| Salvar | "Salvando..." | "Processando...", "Aguarde..." |
| Gerar documento | "Gerando documento..." | "Processando...", "Aguarde..." |
| Exportar | "Exportando..." | "Gerando arquivo..." |
| Excluir | "Excluindo..." | "Removendo...", "Processando..." |
| Importar | "Importando dados..." | "Carregando arquivo..." |

### Sucesso

Confirmar o que foi feito, de forma especifica.

| Acao | Mensagem de sucesso |
|------|---------------------|
| Salvar funcionario | "Funcionario salvo com sucesso." |
| Criar template | "Template criado com sucesso." |
| Excluir registro | "Registro excluido com sucesso." |
| Gerar documento | "Documento gerado com sucesso." |
| Exportar | "Arquivo exportado com sucesso." |
| Importar | "Importacao concluida. X registros importados." |

**Regras:**
- Frases curtas, no passado
- Ponto final
- Sem exclamacao (exceto em onboarding ou acoes muito importantes)

### Erro

Explicar o que aconteceu e, quando possivel, o que o usuario pode fazer.

**Estrutura:** `[O que nao foi possivel fazer]. [Causa, se conhecida]. [O que fazer, se aplicavel].`

| Situacao | Mensagem de erro |
|----------|-----------------|
| Erro ao salvar (generico) | "Nao foi possivel salvar. Verifique sua conexao e tente novamente." |
| Erro ao carregar lista | "Nao foi possivel carregar os dados. Tente novamente." |
| Erro ao gerar documento | "Nao foi possivel gerar o documento. Verifique se o template e valido." |
| Registro nao encontrado | "Nenhum registro encontrado para os filtros aplicados." |
| Campo obrigatorio vazio | "Nome e obrigatorio." (no campo, nao em alerta global) |
| CPF invalido | "CPF deve estar no formato 000.000.000-00." |
| Data invalida | "Data invalida. Use o formato DD/MM/AAAA." |
| Sem permissao | "Voce nao tem permissao para realizar esta acao." |

**Regras:**
- Nunca expor codigo de erro tecnico ao usuario (HTTP 500, stacktrace)
- Nao culpar o usuario
- Nao usar "ERRO:" no inicio — o alerta vermelho ja comunica isso
- Sempre com ponto final

### Aviso (Warning)

Para situacoes que precisam de atencao mas nao bloqueiam o usuario.

```
"Este template nao foi usado nos ultimos 90 dias. Verifique se ele ainda e valido."
"Existem funcionarios sem jornada configurada."
"A competencia selecionada esta fechada. Alteracoes podem nao ser aplicadas."
```

### Informativo

Para contexto adicional util, sem urgencia.

```
"Os documentos gerados ficam disponiveis por 30 dias."
"Apenas templates do tipo Word aceitam coringas de jornada."
```

---

## 6. Estados Vazios

Exibidos quando uma lista nao tem resultados. Seguem a estrutura:

```
[Icone]
[Titulo]
[Mensagem]
[Sub-mensagem ou acao, opcional]
```

### Tipos de estado vazio

**Sem registros cadastrados (ainda nao existe nada):**
```
Titulo:    "Nenhum template cadastrado"
Mensagem:  "Crie o primeiro template para comecar a gerar documentos."
Acao:      [+ Criar Template]
```

**Sem resultados para os filtros aplicados:**
```
Titulo:    "Nenhum resultado encontrado"
Mensagem:  "Nenhum funcionario corresponde aos filtros aplicados."
Sub:       "Tente ajustar a busca ou os filtros."
```

**Selecao necessaria (painel lateral vazio):**
```
Titulo:    "Selecione um documento"
Mensagem:  "Clique em um documento na lista para ver os detalhes."
```

**Regras:**
- Titulo: substantivo + predicado — "Nenhum [X] encontrado" ou "Nenhum [X] cadastrado"
- Mensagem: explicar por que esta vazio e o que fazer
- Nao usar "Ops!", "Hmm...", ou linguagem muito informal
- Quando possivel, incluir acao direta (botao "Criar X")

---

## 7. Formatacao de Dados

Padrao de exibicao de valores em toda a aplicacao.

### Datas

| Contexto | Formato | Exemplo |
|----------|---------|---------|
| Data completa (dia/mes/ano) | DD/MM/AAAA | 15/03/2026 |
| Competencia (mes/ano) | MM/AAAA | 03/2026 |
| Data e hora | DD/MM/AAAA HH:mm | 15/03/2026 09:32 |
| Intervalo de datas | DD/MM/AAAA a DD/MM/AAAA | 01/01/2026 a 31/01/2026 |
| Data relativa (lista/historico) | "hoje", "ontem", "X dias" para datas recentes | "hoje", "3 dias", "15/02/2026" |

**Data relativa:** usar para datas dentro dos ultimos 7 dias em listas de atividades/historico. Para datas anteriores, usar o formato DD/MM/AAAA.

### Valores monetarios

| Contexto | Formato | Exemplo |
|----------|---------|---------|
| Valor padrao | R$ com separador de milhar e 2 casas decimais | R$ 5.200,00 |
| Valor negativo | Sinal antes do R$ | R$ -1.200,00 |
| Totalizadores | Mesmo formato | R$ 312.450,90 |

**Separadores:** ponto para milhar (`.`), virgula para decimal (`,`) — padrao brasileiro.

### CPF e CNPJ

| Documento | Formato | Exemplo |
|-----------|---------|---------|
| CPF | 000.000.000-00 | 123.456.789-00 |
| CNPJ | 00.000.000/0000-00 | 12.345.678/0001-90 |
| CPF/CNPJ (campo unificado) | Formatacao automatica pelo tipo | — |

### Telefone

| Tipo | Formato | Exemplo |
|------|---------|---------|
| Celular | (00) 00000-0000 | (11) 98765-4321 |
| Fixo | (00) 0000-0000 | (11) 3456-7890 |

### CEP

```
00000-000  →  Exemplo: 01310-100
```

### Numeros ordinais (competencia, matricula)

- Matricula: sem zeros a esquerda na exibicao (exceto quando o sistema usa codigos fixos)
- Competencia: sempre MM/AAAA — nunca "Marco 2026" ou "2026-03"

### Percentuais

```
Formato: 00,00%  →  Exemplo: 12,50%
Sempre com duas casas decimais em campos financeiros.
```

### Horas e duracao

| Contexto | Formato | Exemplo |
|----------|---------|---------|
| Hora do dia | HH:mm | 08:00, 17:30 |
| Duracao em horas | HHh MMmin | 8h 00min, 12h 36min |
| Total de horas semanais | HH:mm | 44:00 |

---

## 8. Placeholders e Textos de Ajuda

### Placeholder (texto dentro do campo vazio)

Deve descrever **o formato esperado** ou **um exemplo** — nunca repetir o label.

| Label | Placeholder correto | Placeholder errado |
|-------|--------------------|--------------------|
| Nome Completo | "Ex: Joao Carlos da Silva" | "Nome Completo", "Digite o nome" |
| CPF | "000.000.000-00" | "CPF do funcionario", "Informe o CPF" |
| Competencia | "MM/AAAA" | "Competencia", "Digite o mes e ano" |
| Buscar | "Buscar por nome ou CPF..." | "Digite aqui", "Pesquisar" |
| Email | "nome@empresa.com.br" | "Email", "Informe o email" |
| CNPJ | "00.000.000/0000-00" | "CNPJ da empresa" |

**Regras:**
- Comeca com letra minuscula (exceto exemplos com nomes proprios)
- Nao termina com ponto
- Para busca global: sempre terminar com "..." para indicar que e um campo de busca

### Texto de ajuda (abaixo do campo)

Para instrucoes adicionais que nao cabem no label. Aparece sempre, nao so no erro.

```
Label:   Salario Base *
Campo:   [____________]
Ajuda:   Valor bruto, sem descontos. Use virgula para decimal.
```

```
Label:   Template *
Campo:   [     v  ]
Ajuda:   Apenas templates do tipo Word suportam coringas.
```

**Regras:**
- Fonte menor (`CaptionText`, 12px, `TextLightColor`)
- Uma linha, concisa
- Sem redundancia com o label

---

## 9. Confirmacoes e Dialogs

### Dialog de confirmacao de exclusao

Estrutura padrao:

```
Titulo:    "Excluir [nome do item]?"
Mensagem:  "Esta acao nao pode ser desfeita."
Detalhe:   (opcional) "O template '[nome]' sera removido permanentemente."
Botoes:    [Cancelar]  [Excluir]   ← Excluir em DangerColor
```

**Nao usar:**
- "Tem certeza?" — condescendente
- "Deseja realmente excluir?" — redundante
- "Sim" / "Nao" — ambiguos

### Dialog de confirmacao de cancelamento (formulario com dados)

```
Titulo:    "Descartar alteracoes?"
Mensagem:  "As alteracoes feitas nao foram salvas e serao perdidas."
Botoes:    [Continuar editando]  [Descartar]   ← Descartar em DangerColor
```

### Dialog de acao importante (nao destrutiva)

```
Titulo:    "Confirmar geracao de documentos"
Mensagem:  "Serao gerados X documentos para Y funcionarios da empresa [Nome]."
Detalhe:   "Esta acao pode demorar alguns minutos."
Botoes:    [Cancelar]  [Confirmar]
```

### Regras gerais de confirmacao

- Titulo como pergunta direta ("Excluir template?") ou frase nominal ("Confirmar exclusao")
- Mensagem explica a consequencia principal
- Botao de acao destrutiva usa `DangerColor`, confirmativos usam `PrimaryColor`
- Botao seguro sempre a esquerda, acao principal a direita

---

## 10. Referencia Rapida

### Verbos padrao por acao

| Acao | Verbo |
|------|-------|
| Criar | Criar, Novo |
| Ler/visualizar | Ver, Visualizar, Abrir |
| Atualizar | Editar, Salvar |
| Excluir | Excluir |
| Buscar | Buscar |
| Exportar | Exportar |
| Importar | Importar |
| Gerar | Gerar |
| Enviar | Enviar |
| Baixar | Baixar, Fazer download |

### Terminologia do dominio

| Conceito | Termo correto | Nao usar |
|----------|--------------|---------|
| Pessoa que trabalha na empresa | Funcionario | Colaborador, Empregado |
| Empresa empregadora | Empresa | Empregador, Companhia |
| Mes de referencia da folha | Competencia | Periodo, Mes, Referencia |
| Numero do funcionario | Matricula | Codigo, ID, Registro |
| Modelo de documento | Template | Modelo, Formulario |
| Placeholder do template | Coringa | Campo, Placeholder, Tag |
| Verba da folha | Rubrica | Evento (exceto contexto tecnico), Lancamento |
| Desconto do funcionario | Rubrica de desconto | Deducao |
| Valor pago ao funcionario | Rubrica de vencimento | Credito |

### Capitalizacao — resumo

| Elemento | Regra | Exemplo |
|----------|-------|---------|
| Titulo de pagina | Primeira letra maiuscula | "Funcionarios" |
| Label de campo | Primeira letra maiuscula | "Nome Completo" |
| Botao | Primeira letra maiuscula | "Salvar alteracoes" |
| Mensagem | Primeira letra maiuscula, ponto final | "Documento salvo com sucesso." |
| Placeholder | Minusculo (exceto exemplo com nome) | "buscar por nome..." |
| Termos do dominio em texto corrido | Minusculo | "o funcionario foi salvo" |

---

*Anterior: [05-Icones.md](05-Icones.md)*
