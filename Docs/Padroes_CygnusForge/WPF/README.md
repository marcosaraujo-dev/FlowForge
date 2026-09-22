# WPF — Implementação Técnica

Esta seção contém documentação específica para desenvolvimento de interfaces WPF com .NET Framework 4.8.

> **Esta seção é para desenvolvedores WPF.** Para padrões visuais e de componentes aplicáveis a qualquer framework, consulte [Fundamentos](../01-Fundamentos/) e [Componentes](../02-Componentes/).

---

## Conteúdo desta seção

| Documento | Descrição | Quando consultar |
|-----------|-----------|-----------------|
| [01-MVVM.md](01-MVVM.md) | Padrão MVVM com CommunityToolkit.Mvvm | Ao criar View/ViewModel |
| [02-CicloDeVida.md](02-CicloDeVida.md) | Ciclo de vida de Views e ViewModels | Ao debugar problemas de inicialização |
| [03-PadroesObrigatorios.md](03-PadroesObrigatorios.md) | Checklist de PR, regras obrigatórias | Antes de submeter código para revisão |
| [04-AntiPatterns.md](04-AntiPatterns.md) | O que nunca fazer — exemplos de código errado/correto | Durante code review |
| [05-Converters.md](05-Converters.md) | Quando usar Converter vs Behavior vs Property calculada | Ao implementar lógica de apresentação |
| [06-Templates.md](06-Templates.md) | Templates prontos de View, ViewModel, Converter | Ao criar novo arquivo |
| [07-Componentes-Especificos.md](07-Componentes-Especificos.md) | DiagnosticPanel, LookupComboBox, WizardStepper | Ao usar componentes exclusivos WPF |

---

## Stack tecnológica

| Item | Tecnologia |
|------|-----------|
| Framework | .NET Framework 4.8 |
| UI | WPF (Windows Presentation Foundation) |
| Padrão arquitetural | MVVM |
| Library MVVM | CommunityToolkit.Mvvm 8.x |
| Logging | Serilog |
| Icones | Segoe MDL2 Assets (fonte nativa Windows) |

---

## Como o Design System é aplicado no WPF

Os tokens de design ficam em arquivos XAML na pasta `Theme/`:

```
Theme/
├── Colors.xaml       ← Todas as cores (PrimaryColor, SuccessColor, etc.)
├── Typography.xaml   ← Estilos de texto (PageTitle, FieldLabel, etc.)
├── Spacing.xaml      ← Tokens de espaçamento (Padding.Card, Height.Button.Medium, etc.)
└── All.xaml          ← Importa todos os anteriores

Controls/
├── Buttons.xaml      ← Estilos de botões
├── Badges.xaml       ← Estilos de badges
├── Cards.xaml        ← Estilos de cards
├── Inputs.xaml       ← TextBox, ComboBox, DatePicker
├── DataGrid.xaml     ← DataGrid com estilos padronizados
├── Alerts.xaml       ← Caixas de alerta
└── ScrollBars.xaml   ← Scrollbar customizada (estilo global implícito)
```

Para usar um token na View:
```xml
<!-- Cor -->
<Button Background="{StaticResource PrimaryColor}"/>

<!-- Tipografia -->
<TextBlock Style="{StaticResource PageTitle}" Text="Título da Página"/>

<!-- Espaçamento -->
<Border Padding="{StaticResource Padding.Card}"/>
<Button Height="{StaticResource Height.Button.Medium}"/>
```

---

## Fluxo de leitura recomendado

**Desenvolvedor novo no time:**
```
1. Fundamentos/00-Tokens.md         (tokens disponíveis)
2. WPF/01-MVVM.md                   (padrão arquitetural)
3. WPF/03-PadroesObrigatorios.md    (regras obrigatórias)
4. WPF/02-CicloDeVida.md            (entender o ciclo View/VM)
5. WPF/04-AntiPatterns.md           (o que evitar)
6. WPF/06-Templates.md              (templates prontos)
```

**Code review:**
```
1. WPF/03-PadroesObrigatorios.md    (checklist de PR)
2. WPF/04-AntiPatterns.md           (verificar anti-patterns)
```
