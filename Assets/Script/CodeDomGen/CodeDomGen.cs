using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class CodeDomGen
{
    private string _ns = "HuangDAPI";
    private string _className;
    private List<Tuple<string, Type, Type, List<object>>> _fieldsDictionary;

    private string _sourceCode;

    private CodeCompileUnit _targetUnit;
    private CodeTypeDeclaration _targetClass;
    public CodeDomGen(string className, List<Tuple<string, Type, Type, List<object>>> fieldsDictionary, string[] namespaces = null)
    {
        _fieldsDictionary = fieldsDictionary;
        _className = className;

        _targetUnit = new CodeCompileUnit();
        CodeNamespace ns = new CodeNamespace(_ns);
        ns.Imports.Add(new CodeNamespaceImport("System"));
        if (namespaces != null)
        {
            foreach(var value in namespaces)
            {
                ns.Imports.Add(new CodeNamespaceImport(value));
            }
        }
        _targetClass = new CodeTypeDeclaration(className);
        _targetClass.IsClass = true;
        _targetClass.TypeAttributes = TypeAttributes.Public;
        //_targetClass.BaseTypes.Add("Office");
        ns.Types.Add(_targetClass);
        _targetUnit.Namespaces.Add(ns);
    }

    public string SourceCode
    {
        get { return _sourceCode; }
    }

    public string TypeName
    {
        get
        {
            return string.Format("{0}.{1}", _ns, _className);
        }
    }

    public string Create()
    {
        AddFields();

        AddStaticCtor();

        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        CodeGeneratorOptions options = new CodeGeneratorOptions();
        options.BracingStyle = "C";

        using (StringWriter sourceWriter = new StringWriter())
        {
            provider.GenerateCodeFromCompileUnit(_targetUnit, sourceWriter, options);
            _sourceCode = sourceWriter.ToString();
        }
        return _sourceCode;

    }

    private void AddFields()
    {
        // Declare  fields .
        foreach (var kv in _fieldsDictionary)
        {
            CodeMemberField widthValueField = new CodeMemberField();
            widthValueField.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            widthValueField.Name = kv.Item1;
            widthValueField.Type = new CodeTypeReference(kv.Item2);
            _targetClass.Members.Add(widthValueField);
        }
    }

    private void AddStaticCtor()
    {
        CodeTypeConstructor constructor = new CodeTypeConstructor();
        constructor.Attributes = MemberAttributes.Public;

        foreach (var kv in _fieldsDictionary)
        {
            CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(null, kv.Item1);

            CodeObjectCreateExpression right = new CodeObjectCreateExpression();

            right.CreateType = new CodeTypeReference(kv.Item3);

            if(kv.Item4 != null)
            {
                foreach (object param in kv.Item4)
                {
                    if (param is Enum)
                    {
                        CodeFieldReferenceExpression codeEnum = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(param.GetType().Name), ((Enum)param).ToString());
                        right.Parameters.Add(codeEnum);
                    }
                    else
                    {
                        right.Parameters.Add(new CodePrimitiveExpression(param));
                    }

                }
            }

            constructor.Statements.Add(new CodeAssignStatement(left, right));
        }

        _targetClass.Members.Add(constructor);

        //// Declare constructor
        //CodeConstructor constructor = new CodeConstructor();
        //constructor.Attributes = MemberAttributes.Public | MemberAttributes.Static;

        //// Add parameters.
        //foreach (var kv in _fieldsDictionary)
        //{
        //    constructor.Parameters.Add(new CodeParameterDeclarationExpression(kv.Key, kv.Value));
        //}

        //// Add field initialization logic
        //foreach (var kv in _fieldsDictionary)
        //{
        //    CodeFieldReferenceExpression reference = new CodeFieldReferenceExpression( new CodeThisReferenceExpression(), kv.Value);
        //    constructor.Statements.Add(new CodeAssignStatement(reference, new CodeArgumentReferenceExpression(kv.Value)));
        //}

        //_targetClass.Members.Add(constructor);
    }
}
