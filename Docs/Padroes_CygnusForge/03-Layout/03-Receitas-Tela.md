# Receitas de Tela

Blueprints completos para as telas mais comuns dos sistemas da empresa. Cada receita combina os tokens, componentes e padroes de layout em uma estrutura pronta para ser implementada — identica em WPF e React.

> **Como usar:** Identifique o tipo de tela que precisa criar, copie o blueprint e substitua pelos seus dados.

---

## Indice

1. [Lista + CRUD](#1-lista--crud)
2. [Cadastro e Edicao](#2-cadastro-e-edicao)
3. [Detalhe (Somente Leitura)](#3-detalhe-somente-leitura)
4. [Dashboard / Resumo](#4-dashboard--resumo)
5. [Wizard (Fluxo Guiado)](#5-wizard-fluxo-guiado)
6. [Lista com Painel Lateral](#6-lista-com-painel-lateral)

---

## 1. Lista + CRUD

**Quando usar:** Exibir registros em tabela com opcoes de criar, editar e excluir. E o padrao mais comum nos sistemas da empresa.

**Exemplos:** Lista de Funcionarios, Templates, Empresas, Documentos Gerados.

### Blueprint

```
+-- Header -------------------------------------------------------+
|  Funcionarios                              [ + Novo Funcionario ]|
+-----------------------------------------------------------------+

+-- Toolbar -------------------------------------------------------+
|  [ Buscar por nome ou CPF...    ]    [ Empresa v ]  [ Status v ] |
+-----------------------------------------------------------------+

+-- DataGrid ------------------------------------------------------+
|  Nome                CPF             Empresa     Status  Acoes   |
|  --------------------------------------------------------        |
|  Joao da Silva       123.456.789-00  Alfa SA     Ativo   [/][x]  |
|  Maria Costa         987.654.321-00  Beta Ltda   Ativo   [/][x]  |
|  Carlos Nunes        111.222.333-00  Alfa SA     Inativo [/][x]  |
|  ...                                                             |
+-----------------------------------------------------------------+

+-- Rodape de Paginacao -------------------------------------------+
|  Mostrando 1-20 de 62 registros        [<][1][2][3][>]          |
+-----------------------------------------------------------------+
```

**Quando lista esta vazia:** Substituir o DataGrid pelo componente `DataNotFound`.

### Componentes utilizados

| Componente | Doc |
|------------|-----|
| Header de pagina com acao | [01-Estrutura-Paginas.md](01-Estrutura-Paginas.md) |
| Campo de busca | [04-Formularios.md](../02-Componentes/04-Formularios.md) |
| Filtros em ComboBox | [04-Formularios.md](../02-Componentes/04-Formularios.md) |
| DataGrid / Tabela | [05-Tabelas.md](../02-Componentes/05-Tabelas.md) |
| Badges de status | [02-Badges-Status.md](../02-Componentes/02-Badges-Status.md) |
| Botoes de acao por linha | [01-Botoes.md](../02-Componentes/01-Botoes.md) |
| Estado vazio (DataNotFound) | [10-Loading-Estados.md](../02-Componentes/10-Loading-Estados.md) |

### Regras desta receita

- O botao "Novo" fica **no header**, alinhado a direita — nunca dentro da toolbar de filtros
- Filtros ficam em toolbar separada abaixo do header
- Campo de busca ocupa toda a largura disponivel da toolbar (exceto espaco dos filtros)
- Acoes por linha (editar/excluir) ficam na **ultima coluna** do DataGrid, alinhadas ao centro
- Botao Excluir sempre abre **Dialog de confirmacao** antes de executar
- Estado vazio aparece somente apos o carregamento — nunca antes

### Implementacao WPF

```xml
<Grid>
    <ScrollViewer HorizontalScrollBarVisibility="Disabled">
        <StackPanel Margin="{StaticResource Margin.Container}">

            <!-- Header -->
            <Border Style="{StaticResource PageTitleContainer}">
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="Auto"/>
                    </Grid.ColumnDefinitions>
                    <TextBlock Grid.Column="0" Style="{StaticResource PageTitle}" Text="Funcionarios"/>
                    <Button Grid.Column="1"
                            Command="{Binding NovoCommand}"
                            Content="+ Novo Funcionario"
                            Style="{StaticResource MediumPrimaryButton}"/>
                </Grid>
            </Border>

            <!-- Toolbar de filtros -->
            <Border Style="{StaticResource CardContainer}" Margin="{StaticResource Margin.Between.Items}">
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="200"/>
                        <ColumnDefinition Width="150"/>
                    </Grid.ColumnDefinitions>
                    <TextBox Grid.Column="0"
                             Text="{Binding Filtro, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                             Style="{StaticResource RoundedTextBox}"
                             Tag="Buscar por nome ou CPF..."
                             Margin="0,0,8,0"/>
                    <ComboBox Grid.Column="1"
                              ItemsSource="{Binding Empresas}"
                              SelectedItem="{Binding EmpresaFiltro, Mode=TwoWay}"
                              Style="{StaticResource RoundedComboBox}"
                              Margin="0,0,8,0"/>
                    <ComboBox Grid.Column="2"
                              ItemsSource="{Binding StatusOpcoes}"
                              SelectedItem="{Binding StatusFiltro, Mode=TwoWay}"
                              Style="{StaticResource RoundedComboBox}"/>
                </Grid>
            </Border>

            <!-- DataGrid ou estado vazio -->
            <Border Style="{StaticResource CardContainer}">
                <Grid>
                    <DataGrid ItemsSource="{Binding Itens}"
                              SelectedItem="{Binding ItemSelecionado, Mode=TwoWay}"
                              Style="{StaticResource DefaultDataGrid}"
                              RowStyle="{StaticResource DefaultDataGridRow}"
                              Visibility="{Binding TemItens, Converter={StaticResource BoolToVisibility}}"
                              AutoGenerateColumns="False">
                        <DataGrid.Columns>
                            <DataGridTextColumn Header="Nome" Binding="{Binding Nome}" Width="*"/>
                            <DataGridTextColumn Header="CPF" Binding="{Binding Cpf}" Width="130"/>
                            <!-- ... demais colunas ... -->
                            <DataGridTemplateColumn Header="" Width="70" CanUserSort="False">
                                <DataGridTemplateColumn.CellTemplate>
                                    <DataTemplate>
                                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
                                            <Button Command="{Binding DataContext.EditarCommand, RelativeSource={RelativeSource AncestorType=DataGrid}}"
                                                    CommandParameter="{Binding}"
                                                    Content="&#xE70F;"
                                                    Style="{StaticResource GridIconButton}">
                                                <Button.ToolTip><ToolTip><TextBlock Text="Editar"/></ToolTip></Button.ToolTip>
                                            </Button>
                                            <Button Command="{Binding DataContext.ExcluirCommand, RelativeSource={RelativeSource AncestorType=DataGrid}}"
                                                    CommandParameter="{Binding}"
                                                    Content="&#xE74D;"
                                                    Style="{StaticResource GridIconButtonDanger}">
                                                <Button.ToolTip><ToolTip><TextBlock Text="Excluir"/></ToolTip></Button.ToolTip>
                                            </Button>
                                        </StackPanel>
                                    </DataTemplate>
                                </DataGridTemplateColumn.CellTemplate>
                            </DataGridTemplateColumn>
                        </DataGrid.Columns>
                    </DataGrid>

                    <components:DataNotFound
                        Visibility="{Binding ListaVazia, Converter={StaticResource BoolToVisibility}}"
                        Title="Nenhum funcionario encontrado"
                        Message="Nenhum funcionario corresponde aos filtros aplicados."
                        SubMessage="Tente ajustar a busca ou os filtros."/>
                </Grid>
            </Border>

        </StackPanel>
    </ScrollViewer>
</Grid>
```

### Implementacao HTML / React

```html
<div class="page-container">

  <!-- Header -->
  <div class="page-header">
    <h1 class="page-title">Funcionarios</h1>
    <button class="btn btn-primary btn-md">+ Novo Funcionario</button>
  </div>

  <!-- Toolbar -->
  <div class="card toolbar">
    <input class="input-text" type="text" placeholder="Buscar por nome ou CPF..." />
    <select class="select-rounded"><!-- opcoes de empresa --></select>
    <select class="select-rounded"><!-- opcoes de status --></select>
  </div>

  <!-- Tabela -->
  <div class="card">
    <table class="data-table">
      <thead>
        <tr>
          <th>Nome</th>
          <th>CPF</th>
          <th>Empresa</th>
          <th>Status</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>Joao da Silva</td>
          <td>123.456.789-00</td>
          <td>Alfa SA</td>
          <td><span class="badge badge-success">Ativo</span></td>
          <td class="actions">
            <button class="btn-icon" title="Editar">&#xE70F;</button>
            <button class="btn-icon btn-icon-danger" title="Excluir">&#xE74D;</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Estado vazio (quando lista for vazia) -->
    <!-- <div class="data-not-found">...</div> -->
  </div>

</div>
```

### Checklist

- [ ] Botao "Novo" no header, nao na toolbar
- [ ] Campo de busca com `UpdateSourceTrigger=PropertyChanged` (WPF) / `onChange` (React)
- [ ] Estado vazio visivel quando lista esta vazia
- [ ] Botao excluir abre confirmacao antes de agir
- [ ] Loading ao carregar/filtrar
- [ ] Badges de status usando as cores corretas do DS

---

## 2. Cadastro e Edicao

**Quando usar:** Formulario para criacao de novo registro ou edicao de existente. Pode ser uma pagina completa ou um Dialog.

**Exemplos:** Cadastro de Template, Editar dados de Funcionario, Configuracoes de Empresa.

### Blueprint

```
+-- Header -------------------------------------------------------+
|  [<] Editar Funcionario                                         |
|  Joao da Silva - Matricula 001                                  |
+-----------------------------------------------------------------+

+-- Secao: Dados Pessoais -----------------------------------------+
|  [ Nome Completo *           ]   [ CPF *          ]             |
|  [ Data de Nascimento        ]   [ RG             ]             |
+-----------------------------------------------------------------+

+-- Secao: Dados Contratuais --------------------------------------+
|  [ Empresa *    v]           [ Matricula          ]             |
|  [ Data Admissao * ]         [ Cargo              ]             |
|  [ Salario Base    ]         [ Jornada        v   ]             |
+-----------------------------------------------------------------+

+-- Secao: Endereco -----------------------------------------------+
|  [ Logradouro                    ]   [ Numero ]   [ Compl.  ]   |
|  [ Municipio                 ]   [ UF v ]   [ CEP           ]   |
+-----------------------------------------------------------------+

+-- Footer de Acoes -----------------------------------------------+
|                              [ Cancelar ]   [ Salvar Alteracoes ]|
+-----------------------------------------------------------------+
```

### Componentes utilizados

| Componente | Doc |
|------------|-----|
| Header com breadcrumb / subtitulo | [01-Estrutura-Paginas.md](01-Estrutura-Paginas.md) |
| Secoes de formulario | [13-Secoes-Formulario.md](../02-Componentes/13-Secoes-Formulario.md) |
| Campos de entrada | [04-Formularios.md](../02-Componentes/04-Formularios.md) |
| Footer de acoes | [01-Estrutura-Paginas.md](01-Estrutura-Paginas.md) |
| Alertas de validacao | [06-Alertas.md](../02-Componentes/06-Alertas.md) |

### Regras desta receita

- Header inclui botao/link de voltar (`[<]`) quando e uma pagina dedicada
- Campos obrigatorios marcados com `*` em `DangerColor`
- Secoes com mais de 6 campos devem ser divididas por separadores nomeados
- Footer **fixo** na parte inferior — nao rola com o conteudo
- Botao principal a direita ("Salvar"), cancelar a esquerda
- Ao salvar com sucesso: exibir alerta de sucesso e navegar de volta ou permanecer com dados atualizados
- Ao salvar com erro: exibir alerta de erro, manter dados preenchidos

### Implementacao WPF

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="*"/>      <!-- conteudo rolavel -->
        <RowDefinition Height="Auto"/>   <!-- footer fixo -->
    </Grid.RowDefinitions>

    <!-- Conteudo rolavel -->
    <ScrollViewer Grid.Row="0" HorizontalScrollBarVisibility="Disabled">
        <StackPanel Margin="{StaticResource Margin.Container}">

            <!-- Header -->
            <Border Style="{StaticResource PageTitleContainer}">
                <StackPanel>
                    <TextBlock Style="{StaticResource PageTitle}" Text="Editar Funcionario"/>
                    <TextBlock Style="{StaticResource SecondaryText}" Text="{Binding Subtitulo}"/>
                </StackPanel>
            </Border>

            <!-- Secao: Dados Pessoais -->
            <Border Style="{StaticResource CardContainer}">
                <StackPanel>
                    <TextBlock Style="{StaticResource SectionTitle}" Text="Dados Pessoais"/>
                    <Separator Margin="0,8,0,16" Background="{StaticResource BorderColor}"/>

                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="16"/>
                            <ColumnDefinition Width="*"/>
                        </Grid.ColumnDefinitions>

                        <!-- Coluna esquerda -->
                        <StackPanel Grid.Column="0">
                            <TextBlock Style="{StaticResource FieldLabel}" Text="Nome Completo *"/>
                            <TextBox Text="{Binding Nome, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                                     Style="{StaticResource RoundedTextBox}"
                                     Margin="{StaticResource Margin.Label.To.Input}"/>
                        </StackPanel>

                        <!-- Coluna direita -->
                        <StackPanel Grid.Column="2">
                            <TextBlock Style="{StaticResource FieldLabel}" Text="CPF *"/>
                            <TextBox Text="{Binding Cpf, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                                     Style="{StaticResource RoundedTextBox}"
                                     Margin="{StaticResource Margin.Label.To.Input}"/>
                        </StackPanel>
                    </Grid>
                </StackPanel>
            </Border>

            <!-- Demais secoes... -->

        </StackPanel>
    </ScrollViewer>

    <!-- Footer fixo -->
    <Border Grid.Row="1"
            BorderThickness="0,1,0,0"
            BorderBrush="{StaticResource BorderColor}"
            Background="{StaticResource SurfaceColor}"
            Padding="24,14">
        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
            <Button Command="{Binding CancelarCommand}"
                    Content="Cancelar"
                    Style="{StaticResource MediumSecondaryButton}"
                    Margin="0,0,8,0"/>
            <Button Command="{Binding SalvarCommand}"
                    Content="Salvar Alteracoes"
                    Style="{StaticResource MediumPrimaryButton}"/>
        </StackPanel>
    </Border>
</Grid>
```

### Implementacao HTML / React

```html
<div class="page-container page-form">

  <!-- Header -->
  <div class="page-header">
    <h1 class="page-title">Editar Funcionario</h1>
    <p class="page-subtitle">Joao da Silva - Matricula 001</p>
  </div>

  <!-- Formulario rolavel -->
  <div class="form-scroll-area">

    <!-- Secao -->
    <div class="card">
      <h2 class="section-title">Dados Pessoais</h2>
      <hr class="divider" />

      <div class="form-grid cols-2">
        <div class="field">
          <label class="field-label">Nome Completo <span class="required">*</span></label>
          <input class="input-text" type="text" />
        </div>
        <div class="field">
          <label class="field-label">CPF <span class="required">*</span></label>
          <input class="input-text" type="text" />
        </div>
      </div>
    </div>

    <!-- Demais secoes... -->

  </div>

  <!-- Footer fixo -->
  <div class="form-footer">
    <button class="btn btn-secondary btn-md">Cancelar</button>
    <button class="btn btn-primary btn-md">Salvar Alteracoes</button>
  </div>

</div>
```

### Checklist

- [ ] Footer fixo (nao rola com conteudo)
- [ ] Campos obrigatorios marcados com `*`
- [ ] `ScrollViewer` / area rolavel tem `HorizontalScrollBarVisibility="Disabled"` (WPF)
- [ ] Botao salvar desabilitado somente quando formulario invalido (opcional — ver regras de validacao)
- [ ] Loading ao salvar
- [ ] Alerta de sucesso/erro apos tentativa de salvar
- [ ] Botao cancelar solicita confirmacao se houver dados alterados

---

## 3. Detalhe (Somente Leitura)

**Quando usar:** Exibir informacoes completas de um registro sem edicao inline. Opcoes de editar e excluir aparecem como acoes no header.

**Exemplos:** Detalhe do Funcionario, Visualizar Template, Documento Gerado.

### Blueprint

```
+-- Header -------------------------------------------------------+
|  [<] Joao da Silva                   [ Editar ]  [ Excluir ]   |
|  Matricula 001 - Alfa SA - Ativo                                |
+-----------------------------------------------------------------+

+-- Card: Dados Pessoais ------------------------------------------+
|  Nome Completo          CPF               Data Nascimento        |
|  Joao da Silva          123.456.789-00    15/03/1985             |
+-----------------------------------------------------------------+

+-- Card: Dados Contratuais ---------------------------------------+
|  Empresa      Cargo         Data Admissao    Salario Base        |
|  Alfa SA      Analista      01/06/2019       R$ 5.200,00         |
+-----------------------------------------------------------------+

+-- Card: Historico (opcional) ------------------------------------+
|  [tab: Ferias] [tab: Holerites] [tab: Documentos]               |
|  ... conteudo da aba ativa ...                                   |
+-----------------------------------------------------------------+
```

### Regras desta receita

- Labels de campo sao menores e em `TextSecondaryColor` — valores em `TextPrimaryColor` e peso normal
- Campos lado a lado formam grupos de 3-4 itens por linha
- Acoes (Editar, Excluir) ficam no header, nunca no rodape
- Informacoes historicas ou secundarias ficam em Tabs abaixo das informacoes principais
- Badge de status visivel no subtitulo do header

### Implementacao WPF

```xml
<!-- Header com acoes -->
<Border Style="{StaticResource PageTitleContainer}">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="Auto"/>
        </Grid.ColumnDefinitions>
        <StackPanel Grid.Column="0">
            <TextBlock Style="{StaticResource PageTitle}" Text="{Binding Nome}"/>
            <StackPanel Orientation="Horizontal">
                <TextBlock Style="{StaticResource SecondaryText}" Text="{Binding Subtitulo}"/>
                <Border Style="{StaticResource BadgeSuccess}" Margin="8,0,0,0"
                        Visibility="{Binding IsAtivo, Converter={StaticResource BoolToVisibility}}">
                    <TextBlock Text="Ativo"/>
                </Border>
            </StackPanel>
        </StackPanel>
        <StackPanel Grid.Column="1" Orientation="Horizontal">
            <Button Command="{Binding EditarCommand}"
                    Content="Editar"
                    Style="{StaticResource MediumSecondaryButton}"
                    Margin="0,0,8,0"/>
            <Button Command="{Binding ExcluirCommand}"
                    Content="Excluir"
                    Style="{StaticResource MediumDangerButton}"/>
        </StackPanel>
    </Grid>
</Border>

<!-- Campo somente leitura -->
<StackPanel Margin="0,0,0,12">
    <TextBlock Style="{StaticResource FieldLabel}" Text="Nome Completo"/>
    <TextBlock Style="{StaticResource BodyText}" Text="{Binding Nome}" Margin="0,2,0,0"/>
</StackPanel>
```

### Checklist

- [ ] Acoes no header, nao no rodape
- [ ] Labels menores que valores (`FieldLabel` + `BodyText`)
- [ ] Badge de status no subtitulo
- [ ] Tabs para conteudo historico/secundario
- [ ] Botao excluir sempre com confirmacao

---

## 4. Dashboard / Resumo

**Quando usar:** Visao geral de metricas, totalizadores e atalhos para as principais acoes do modulo.

**Exemplos:** Tela inicial do modulo, Resumo da Folha, Painel de Controle.

### Blueprint

```
+-- Header -------------------------------------------------------+
|  Painel - Folha de Pagamento                Competencia: 03/2026 |
+-----------------------------------------------------------------+

+-- Cards de Totalizadores (linha) -------------------------------+
|  +----------+  +----------+  +----------+  +----------+        |
|  | 62       |  | 58       |  | 4        |  | R$ 312k  |        |
|  | Funcionarios | Ativos  |  | Ferias   |  | Total Folha |      |
|  +----------+  +----------+  +----------+  +----------+        |
+-----------------------------------------------------------------+

+-- Conteudo Principal (2 colunas) -------------------------------+
|  +-- Ultimas Atividades -------+  +-- Acoes Rapidas ----------+ |
|  | * Doc gerado - 14:32        |  | [ Gerar Holerites ]       | |
|  | * Template editado - ontem  |  | [ Exportar Folha  ]       | |
|  | * Novo funcionario - 3d     |  | [ Ver Relatorios  ]       | |
|  +-----------------------------+  +---------------------------+ |
+-----------------------------------------------------------------+
```

### Regras desta receita

- Cards de totalizadores em linha horizontal, largura igual (1/4 cada para 4 cards)
- Cada card tem: icone + numero grande + label descritivo
- Numero grande: `AppFont3XLarge` (24px) ou maior, Bold
- Conteudo abaixo dos cards em grade de 2 colunas (70/30 ou 50/50)
- Nao exibir dados carregando parcialmente — use loading ate tudo estar pronto

### Implementacao WPF

```xml
<ScrollViewer HorizontalScrollBarVisibility="Disabled">
    <StackPanel Margin="{StaticResource Margin.Container}">

        <!-- Header -->
        <Border Style="{StaticResource PageTitleContainer}">
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*"/>
                    <ColumnDefinition Width="Auto"/>
                </Grid.ColumnDefinitions>
                <TextBlock Grid.Column="0" Style="{StaticResource PageTitle}" Text="Painel - Folha"/>
                <TextBlock Grid.Column="1" Style="{StaticResource SecondaryText}"
                           Text="{Binding Competencia, StringFormat='Competencia: {0}'}"/>
            </Grid>
        </Border>

        <!-- Cards de totalizadores -->
        <Grid Margin="{StaticResource Margin.Between.Sections}">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="12"/>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="12"/>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="12"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>

            <!-- Card totalizador (repetir para cada metrica) -->
            <Border Grid.Column="0" Style="{StaticResource CardContainer}">
                <StackPanel HorizontalAlignment="Center">
                    <TextBlock Text="&#xE716;" FontFamily="Segoe MDL2 Assets"
                               FontSize="24" Foreground="{StaticResource PrimaryColor}"
                               HorizontalAlignment="Center" Margin="0,0,0,8"/>
                    <TextBlock Text="{Binding TotalFuncionarios}"
                               FontSize="{StaticResource AppFont3XLarge}"
                               FontWeight="Bold" Foreground="{StaticResource TextPrimaryColor}"
                               HorizontalAlignment="Center"/>
                    <TextBlock Text="Funcionarios" Style="{StaticResource SecondaryText}"
                               HorizontalAlignment="Center"/>
                </StackPanel>
            </Border>
            <!-- ... demais cards nas colunas 2, 4, 6 ... -->
        </Grid>

        <!-- Conteudo secundario (2 colunas) -->
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="2*"/>
                <ColumnDefinition Width="12"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <!-- coluna principal -->
            <Border Grid.Column="0" Style="{StaticResource CardContainer}">
                <!-- lista de atividades -->
            </Border>
            <!-- sidebar de acoes rapidas -->
            <Border Grid.Column="2" Style="{StaticResource CardContainer}">
                <!-- botoes de acao rapida -->
            </Border>
        </Grid>

    </StackPanel>
</ScrollViewer>
```

### Checklist

- [ ] Cards de totalizadores com largura igual
- [ ] Icone + numero + label em cada card
- [ ] Loading ate todos os dados estarem prontos
- [ ] Acoes rapidas na sidebar direita

---

## 5. Wizard (Fluxo Guiado)

**Quando usar:** Fluxo sequencial de etapas onde cada passo depende do anterior. O usuario nao pode pular etapas obrigatorias.

**Exemplos:** Geracao de Documentos, Importacao de Dados, Configuracao Inicial.

### Blueprint

```
+-- Header -------------------------------------------------------+
|  Gerar Documentos                                               |
+-----------------------------------------------------------------+

+-- Indicador de Etapas ------------------------------------------+
|  [1 Empresa] -------- [2 Template] -------- [3 Confirmar]       |
|   (concluido)          (ativo)               (pendente)         |
+-----------------------------------------------------------------+

+-- Conteudo da Etapa Atual --------------------------------------+
|                                                                  |
|  Selecione o template                                            |
|  Escolha o modelo que sera usado para gerar o documento.        |
|                                                                  |
|  [ Acordo de Compensacao    v  ]                                |
|  [ Preview do template...     ]                                 |
|                                                                  |
+-----------------------------------------------------------------+

+-- Navegacao de Etapas ------------------------------------------+
|  [< Anterior]                              [Proximo >]          |
+-----------------------------------------------------------------+
```

### Regras desta receita

- Indicador de etapas sempre visivel no topo, abaixo do header
- Etapas: `Pendente` (cinza) / `Ativa` (azul primario) / `Concluida` (verde com check)
- Botao "Proximo" somente habilitado quando a etapa atual esta valida
- Botao "Anterior" sempre habilitado (exceto na primeira etapa)
- Na ultima etapa, "Proximo" vira "Confirmar" ou "Gerar"
- Conteudo de cada etapa em Card com altura flexivel
- Nao reutilizar o footer de acoes padrao — o wizard tem sua propria navegacao

### Implementacao WPF

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>  <!-- header -->
        <RowDefinition Height="Auto"/>  <!-- stepper -->
        <RowDefinition Height="*"/>     <!-- conteudo -->
        <RowDefinition Height="Auto"/>  <!-- navegacao -->
    </Grid.RowDefinitions>

    <!-- Header -->
    <Border Grid.Row="0" Style="{StaticResource PageTitleContainer}"
            Margin="{StaticResource Margin.Container}">
        <TextBlock Style="{StaticResource PageTitle}" Text="Gerar Documentos"/>
    </Border>

    <!-- Stepper (WizardStepper - ver 07-Componentes-Especificos.md) -->
    <components:WizardStepper Grid.Row="1"
                              Steps="{Binding Etapas}"
                              EtapaAtual="{Binding EtapaAtual, Mode=TwoWay}"
                              Margin="{StaticResource Margin.Container}"/>

    <!-- Conteudo da etapa ativa -->
    <ContentControl Grid.Row="2"
                    Content="{Binding ViewEtapaAtual}"
                    Margin="{StaticResource Margin.Container}"/>

    <!-- Navegacao -->
    <Border Grid.Row="3"
            BorderThickness="0,1,0,0"
            BorderBrush="{StaticResource BorderColor}"
            Background="{StaticResource SurfaceColor}"
            Padding="24,14">
        <Grid>
            <Button HorizontalAlignment="Left"
                    Command="{Binding AnteriorCommand}"
                    Content="&lt; Anterior"
                    Style="{StaticResource MediumSecondaryButton}"/>
            <Button HorizontalAlignment="Right"
                    Command="{Binding ProximoCommand}"
                    Content="{Binding TextoBotaoProximo}"
                    Style="{StaticResource MediumPrimaryButton}"/>
        </Grid>
    </Border>
</Grid>
```

### Checklist

- [ ] Stepper visivel com estado correto em todas as etapas
- [ ] "Proximo" desabilitado enquanto etapa nao esta valida
- [ ] "Anterior" disponivel (exceto etapa 1)
- [ ] Ultimo passo tem resumo do que sera feito antes de confirmar
- [ ] Loading ao confirmar/executar a acao final
- [ ] Navegar para resultado ou lista apos conclusao

---

## 6. Lista com Painel Lateral

**Quando usar:** Lista de itens onde clicar em um item exibe seus detalhes em um painel lateral, sem navegar para outra tela.

**Exemplos:** Caixa de entrada de mensagens, lista de templates com preview, historico de documentos.

### Blueprint

```
+-- Header -------------------------------------------------------+
|  Documentos Gerados                                             |
+-----------------------------------------------------------------+

+-- Lista (40%) ----------------+-- Painel de Detalhe (60%) -----+
|  [ Buscar... ]                |  Acordo de Compensacao         |
|  -------------------------    |  Empresa: Alfa SA              |
|  >> Acordo Comp.  14/03/2026  |  Gerado em: 14/03/2026 09:32  |
|     Holerite Mar  12/03/2026  |                                 |
|     Rescisao      10/03/2026  |  [ Abrir ]  [ Baixar ]  [x]   |
|  -------------------------    |                                 |
|  ...                          |  [preview do documento]        |
+-------------------------------+---------------------------------+
```

### Regras desta receita

- Lista ocupa 35-40% da largura, painel 60-65%
- Item selecionado na lista tem fundo `DataGridRowSelectedBrush`
- Painel lateral tem sua propria barra de acoes (abrir, baixar, fechar)
- Clicar em outro item na lista atualiza o painel sem recarregar a pagina
- Em janelas estreitas: lista ocupa tela toda, clicar em item abre painel como overlay

### Checklist

- [ ] Item ativo destacado na lista
- [ ] Painel lateral atualiza ao mudar selecao
- [ ] Acoes do item no painel, nao na lista
- [ ] Estado vazio no painel quando nenhum item esta selecionado

---

*Anterior: [02-Navegacao.md](02-Navegacao.md) | Proximo: [04-Agrupamento-Filtro.md](04-Agrupamento-Filtro.md)*
