using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MainMenuTemplate1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void animControlsOn()
        {
            if (btnRegister.Text == "zarejestruj")
            {
                await loginEntry.FadeTo(0, 100);
                await passwordFrame.FadeTo(0, 100);
                await rePasswordFrame.FadeTo(0, 0);
                await controlsFrame.FadeTo(0, 100);
                await btnLogin.FadeTo(0, 100);
                await btnRegister.FadeTo(0, 100);

                btnLogin.Text = "zarejestruj";
                btnRegister.Text = "zaloguj";
                await loginEntry.FadeTo(1, 100);
                await passwordFrame.FadeTo(1, 100);
                rePasswordFrame.IsVisible = true;
                await rePasswordFrame.FadeTo(1, 100);
                await controlsFrame.FadeTo(1, 100);
                await btnLogin.FadeTo(1, 100);
                await btnRegister.FadeTo(1, 100);

            }
        }
        private async void wrongPass()
        {
            await passwordEntry.ScaleTo(1.1, 100);
            passwordFrame.BorderColor = Color.Red;
            await passwordEntry.ScaleTo(1, 100);
            await rePasswordEntry.ScaleTo(1.1, 100);
            rePasswordFrame.BorderColor = Color.Red;
            await rePasswordEntry.ScaleTo(1, 100);
        }
        private async void wrongLogin()
        {
            await loginEntry.ScaleTo(1.1, 100);
            loginFrame.BorderColor = Color.Red;
            await loginEntry.ScaleTo(1, 100);
        }
        private async void animControlsOff()
        {
            if (btnRegister.Text == "zaloguj")
            {
                await loginEntry.FadeTo(0, 100);
                await passwordFrame.FadeTo(0, 100);
                await rePasswordFrame.FadeTo(0, 100);
                await controlsFrame.FadeTo(0, 100);
                await btnLogin.FadeTo(0, 100);
                await btnRegister.FadeTo(0, 100);

                btnLogin.Text = "zaloguj";
                btnRegister.Text = "zarejestruj";
                await loginEntry.FadeTo(1, 100);
                await passwordFrame.FadeTo(1, 100);
                rePasswordFrame.IsVisible = false;
                await controlsFrame.FadeTo(1, 100);
                await btnLogin.FadeTo(1, 100);
                await btnRegister.FadeTo(1, 100);

            }
        }

        private async void checkPassRegister()
        {
            if (loginEntry.Text == "" || loginEntry.Text == null)
            {
                wrongLogin();
            }
            else
            {
                await loginEntry.ScaleTo(1.1, 100);
                loginFrame.BorderColor = Color.Lime;
                await loginEntry.ScaleTo(1, 100);

                if (passwordEntry.Text != rePasswordEntry.Text)
                {
                    wrongPass();
                }
                else if (passwordEntry.Text == rePasswordEntry.Text)
                {
                    int checkLog = passwordEntry.Text.Length;
                    if (checkLog >= 8)
                    {
                        if ((passwordEntry.Text).Any(char.IsDigit) && (passwordEntry.Text).Any(char.IsUpper))
                        {
                            var CheckSpecialPass = new Regex("^[a-zA-Z0-9 ]*$");
                            if (CheckSpecialPass.IsMatch(passwordEntry.Text))
                            {
                                wrongPass();
                            }
                            else
                            {
                                await passwordEntry.ScaleTo(1.1, 100);
                                passwordFrame.BorderColor = Color.Lime;
                                await passwordEntry.ScaleTo(1, 100);
                                await rePasswordEntry.ScaleTo(1.1, 100);
                                rePasswordFrame.BorderColor = Color.Lime;
                                await rePasswordEntry.ScaleTo(1, 100);

                            }
                        }
                        else
                        {
                            wrongPass();
                        }
                    }
                    else { wrongPass(); }

                }
            }
        }
        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (btnLogin.Text == "zaloguj") {/*akcja logowania*/ }
            else if (btnLogin.Text == "zarejestruj") { checkPassRegister(); /*akcja rejestrowania*/ }
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (btnRegister.Text == "zarejestruj") { animControlsOn(); }
            else if (btnRegister.Text == "zaloguj") { animControlsOff(); }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkBoxShowPassword.IsChecked == true)
            {
                passwordEntry.IsPassword = false;
                rePasswordEntry.IsPassword = false;
            }
            else 
            {
                passwordEntry.IsPassword = true;
                rePasswordEntry.IsPassword = true;
            }
        }

        private void checkBoxRememberLogData_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkBoxRememberLogData.IsChecked == true)
            {
                Preferences.Set("log", loginEntry.Text);
                Preferences.Set("pass", passwordEntry.Text);
            }
            else
            {
                Preferences.Set("log", "defaultValue");
                Preferences.Set("pass", "defaultValue");
            }
        }
    }
}
