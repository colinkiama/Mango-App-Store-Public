using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Management.Deployment;

namespace installTask
{
    public sealed class install : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        string resultText = "Nothing";
        bool pkgRegistered = false;
        private static IBackgroundTaskInstance boom;
        static double installPercentage = 0;
        /// <summary>
        /// Pretty much identical to showProgressInApp() in MainPage.xaml.cs
        /// </summary>
        /// <param name="taskInstance"></param>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            boom = taskInstance;
            _deferral = taskInstance.GetDeferral();
            ApplicationTriggerDetails details = (ApplicationTriggerDetails)taskInstance.TriggerDetails;
            string packagePath = "";

            packagePath = (string)details.Arguments["packagePath"];
            PackageManager pkgManager = new PackageManager();
            Progress<DeploymentProgress> progressCallback = new Progress<DeploymentProgress>(installProgress);
            notification.SendUpdatableToastWithProgress(0);
           
            
                try
                {
                    var result = await pkgManager.AddPackageAsync(new Uri(packagePath), null, DeploymentOptions.None).AsTask(progressCallback);
                    checkIfPackageRegistered(result, resultText);
                }

                catch (Exception e)
                {
                    resultText = e.Message;
                }


            


            if (pkgRegistered == true)
            {
                notification.showInstallationHasCompleted();
            }
            else
            {
                notification.showError(resultText);
            }


            _deferral.Complete();
        }


        private static void installProgress(DeploymentProgress installProgress)
        {
             installPercentage = installProgress.percentage;
            boom.Progress = (uint)installPercentage;
            notification.UpdateProgress(installPercentage);
        }




        private void checkIfPackageRegistered(DeploymentResult result, string resultText)
        {
            if (result.IsRegistered)
            {
                pkgRegistered = true;
            }
            else
            {
                resultText = result.ErrorText;
            }
        }




    }
}
