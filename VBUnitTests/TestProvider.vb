Imports System.Text
Imports MaskerCore
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestProvider

    <TestMethod()> Public Sub MaskerProvider_Provides()
        Dim MaskFormulaProvider As MaskFormulaProvider = New MaskFormulaProvider()
        Assert.IsNotNull(MaskFormulaProvider.GetMaskFormulas().ToList())

    End Sub

End Class