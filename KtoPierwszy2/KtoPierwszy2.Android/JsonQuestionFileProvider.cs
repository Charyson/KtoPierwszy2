using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KtoPierwszy2.Questions;

namespace KtoPierwszy2.Droid
{
  class JsonQuestionFileProvider : IJsonQuestionFileProvider
  {
    private AssetManager m_androidAssetManager;
    public JsonQuestionFileProvider(AssetManager assetManager)
    {
      m_androidAssetManager = assetManager;
    }

    public string GetJsonAsString()
    {
      string content;
      using (StreamReader sr = new StreamReader(m_androidAssetManager.Open("Questions.json"), Encoding.GetEncoding("Windows-1250")))
      {
        content = sr.ReadToEnd();
      }

      return content;
    }
  }
}