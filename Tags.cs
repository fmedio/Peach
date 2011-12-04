using System;
using System.Collections.Generic;
using System.IO;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public abstract class Tags
    {
        public static Tag A(string href, params string[] children)
        {
            return t("a").Attr("href", href).Add(children);
        }

        public static Tag Body(params IRenderable[] children)
        {
            return t("body").Add(children);
        }

        public static Tag B(params string[] children)
        {
            return t("b").Add(children);
        }

        public static Tag Br()
        {
            return t("br");
        }

        public static IRenderable Concat(params IRenderable[] children)
        {
            return new Renderables(children);
        }

        public static IRenderable Concat(IEnumerable<IRenderable> children)
        {
            return new Renderables(children);
        }

        public static Tag Div(string id, params IRenderable[] children)
        {
            return t("div").Id(id).Add(children);
        }

        public static Tag Form(string method, ILinkable action, params IRenderable[] children)
        {
            return t("form")
                .Attr("method", method)
                .Attr("action", action.Name)
                .Add(children);
        }

        public static Tag H1(string text)
        {
            return t("h1").Add(text);
        }

        public static Tag H2(string text)
        {
            return t("h2").Add(text);
        }

        public static Tag Head(params IRenderable[] children)
        {
            return t("head").Add(children);
        }

        public static Tag Html(params IRenderable[] children)
        {
            return t("html").Add(children);
        }

        public static Tag Input(string type, string name, string value, params IRenderable[] children)
        {
            return t("input")
                .Attr("type", type)
                .Attr("name", name)
                .Attr("value", value)
                .Add(children);
        }

        public static Tag Li(params IRenderable[] children)
        {
            return t("li").Add(children);
        }

        public static Tag Li(params string[] children)
        {
            return t("li").Add(children);
        }

        public static Tag Link(string href, string rel, string type)
        {
            return t("link")
                .Attr("href", href)
                .Attr("rel", rel)
                .Attr("type", type)
                .Add("");
        }

        public static Tag Pre(params string[] children)
        {
            return t("Pre").Add(children);
        }

        public static Tag Pre(IEnumerable<IRenderable> children)
        {
            return t("Pre").Add(children);
        }

        public static Tag Script(string language, string type, string src)
        {
            return t("script")
                .Attr("language", language)
                .Attr("type", type)
                .Attr("src", src)
                .Add("");
        }

        public static Tag Submit(string value, params IRenderable[] children)
        {
            return t("input")
                .Attr("type", "submit")
                .Attr("name", Guid.NewGuid().ToString())
                .Attr("value", value)
                .Add(children);
        }

        public static IRenderable Text(params string[] text)
        {
            return new Text(String.Join("", text));
        }

        public static Tag Title(string text)
        {
            return t("title").Add(text);
        }

        public static Tag Ul(params IRenderable[] children)
        {
            return t("ul").Add(children);
        }


        public static Tag Ul(IEnumerable<IRenderable> children)
        {
            return t("ul").Add(children);
        }

        private static Tag t(string name)
        {
            return new Tag(name);
        }

        

        private class Renderables : IRenderable
        {
            private readonly IEnumerable<IRenderable> _renderables;

            public Renderables(IEnumerable<IRenderable> renderables)
            {
                _renderables = renderables;
            }

            

            public void Render(StreamWriter streamWriter)
            {
                _renderables.ForEach(r => r.Render(streamWriter));
            }

            
        }

        
    }
}