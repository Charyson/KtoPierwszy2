using System;
using System.Collections.Generic;
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
using Ninject.Modules;

namespace KtoPierwszy2.Droid
{
  class AndroidDependenciesModule : NinjectModule
  {
    private readonly AssetManager m_androidAssetManager;
    public AndroidDependenciesModule(AssetManager androidAssetManager)
    {
      m_androidAssetManager = androidAssetManager;
    }

    public override void Load()
    {
      Bind<AssetManager>().ToConstant(m_androidAssetManager);
      Bind<IJsonQuestionFileProvider>().To<JsonQuestionFileProvider>().InSingletonScope();
    }
  }
}