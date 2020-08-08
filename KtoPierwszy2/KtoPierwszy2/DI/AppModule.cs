using KtoPierwszy2.Questions;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace KtoPierwszy2.DI
{
  class AppModule : NinjectModule
  {
    public override void Load()
    {
      Bind<IQuestionProvider>().To<QuestionsFromJsonProvider>().InSingletonScope();
    }
  }
}
