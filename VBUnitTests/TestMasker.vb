Imports System.Text
Imports MaskerCore
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestMasker

    <TestMethod()> Public Sub Digit_Resolves()
        Dim IPADDRESS As String = "AAA.444.BBB.???"
        Dim Actual As String = IPADDRESS.CSMaskCIDR()
        Assert.AreEqual("***.***.***.???", Actual)
    End Sub
    <TestMethod()> Public Sub MaskCode_Resolves()
        Dim Masker As Masker = New Masker("1.2.333.444", "IPADDRESS")
        Assert.IsNotNull(Masker.Masked)
    End Sub
    <TestMethod()> Public Sub MaskExtention_Resolves()
        Dim IPADDRESS As String = "1.2.333.444"
        Dim Actual As String = IPADDRESS.CSMask("IPADDRESS")
        Assert.AreEqual("1.2.***.***", Actual)
    End Sub
    <TestMethod()> Public Sub MaskExtentionDelegate_Resolves()
        Dim IPADDRESS As String = "1.2.333.444"
        Dim Actual As String = IPADDRESS.CSMask(Function(s As String) s.Replace("3", "#"))
        Assert.AreEqual("1.2.###.444", Actual)
    End Sub
    <TestMethod()> Public Sub MaskInlineDelegate_Resolves()
        Dim Masker As Masker = New Masker("333.444", Function(s As String) s.Replace("3", "#"))
        Assert.AreEqual("###.444", Masker.Masked)
    End Sub
    <TestMethod()> Public Sub MaskDelegate_Resolves()
        Dim Masker As Masker = New Masker("333.444", AddressOf MyMask)
        Assert.AreEqual("###.444", Masker.Masked)
    End Sub
    Function MyMask(Input As String) As String
        Input = Input.Replace("3", "#")
        Return Input
    End Function
End Class