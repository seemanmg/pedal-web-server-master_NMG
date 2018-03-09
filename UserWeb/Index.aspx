<%@ Page Language="c#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UserWeb.Index" %>


<html>
<head>
        <!-- Meta -->
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="">
        <meta name="keywords" content="">
        <meta name="author" content="">
        <meta name="googlebot" content="index,follow">
        <!-- Title -->
        <title>Pedal</title>
        <!-- Templates core CSS -->
        <link href="images/bootstrap.css" rel="stylesheet" />
        <link href="images/application.css" rel="stylesheet" />
        <!-- Favicons -->
        <link href="http://pictures.pedal.com/frontend/favicon.png" rel="shortcut icon">
        <link href="http://pictures.pedal.com/frontend/apple-touch-icon-57-precomposed.png" rel="apple-touch-icon">
        <link href="http://pictures.pedal.com/frontend/apple-touch-icon-72-precomposed.png" rel="apple-touch-icon" sizes="72x72">
        <link href="http://pictures.pedal.com/frontend/apple-touch-icon-144-precomposed.png" rel="apple-touch-icon" sizes="114x114">
        <script src="/assets/libs/bootstrap/javascript/vendor/modernizr-2.7.1.min.js"></script>
    </head>
    <body class="index" id="to-top">
        <form id="form1" runat="server">
        <div class="centered ng-scope" >    <!-- Jumbotron -->
            <!-- Jumbotron -->
            <header class="jumbotron" role="banner">
                <div id="home-image-container">
                    &nbsp;
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-md-8">
                            <!-- Logo -->
                            <figure class="text-center logo-home">
                                <a href="./index.html">
                                    <img class="img-logo" src="https://gympictures.blob.core.windows.net/frontend/home-page-logo.png" alt="">
                                </a>
                            </figure> <!-- /.text-center -->
                            <!-- Title -->
                            <h1>Discover Fitness That Fits You</h1>
                            <!-- Sub title -->
                            <p>On-demand access to 300+ gyms, studios, and fitness activities</p>
                        </div> <!-- /.col-md-7 -->
                        <div class="col-md-4 right-phone">
                            <!-- Images showcase -->
                            <figure>
                                <img class="img-iPhone" src="http://pictures.pedal.com/frontend/home-mobile-screen.png" alt="" />
                            </figure>
                        </div> <!-- /.col-md-5 -->
                    </div> <!-- /.row -->
                </div> <!-- /.container -->
            </header> <!-- /.jumbotron -->
            <!-- Services -->
            <section class="services-section" id="section-1">
                <div class="container">
                    <div class="row function-title">
                        <h1>How does it work?</h1>
                        <!-- Row -->
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-services">
                            <!-- Icons -->
                            <figure>
                                <img class="img-noWidth img-margin" src="http://pictures.pedal.com/frontend/user.png" alt="">
                            </figure>
                            <!-- Title -->
                            <h4>Sign Up</h4>
                            <!-- Description -->
                            <p>Join for free and explore fitness options that fit your lifestyle</p>
                        </div> <!-- /.col-md-3 -->
                        <div class="col-md-3 col-services">
                            <!-- Icons -->
                            <figure>
                                <img class="img-noWidth img-margin" src="http://pictures.pedal.com/frontend/search.png" alt="">
                            </figure>
                            <!-- Title -->
                            <h4>Find A Gym</h4>
                            <!-- Description -->
                            <p>Simply search our extensive list of Pedal Partners until you find the perfect match</p>
                        </div> <!-- /.col-md-3 -->
                        <div class="col-md-3 col-services">
                            <!-- Icons -->
                            <figure>
                                <img class="img-noWidth img-margin" src="http://pictures.pedal.com/frontend/basket.png" alt="">
                            </figure>
                            <!-- Title -->
                            <h4>Buy A Pass</h4>
                            <!-- Description -->
                            <p>Choose from a single use day pass or try our unlimited 14-day Pedal Discovery Pass</p>
                        </div> <!-- /.col-md-3 -->
                        <div class="col-md-3 col-services">
                            <!-- Icons -->
                            <figure>
                                <img class="img-noWidth img-margin" src="http://pictures.pedal.com/frontend/iPhone.png" alt="">
                            </figure>
                            <!-- Title -->
                            <h4>Workout</h4>
                            <!-- Description -->
                            <p>Access your pass in the Pedal app and activate when you arrive at the gym</p>
                        </div> <!-- /.col-md-3 -->
                    </div> <!-- /.row -->
                </div> <!-- /.container -->
            </section> <!-- /.services-section -->
            <!-- Features -->
            <section class="home-signup-section" id="section-2">
                <div class="container">
                    <h1>
                        Sign-up for exclusive updates
                    </h1>
                   
                        <div class="row">
                            <div class="col-md-6">
                                <label class="label-form">
                                    First Name <span class="astrik">*</span>
                                </label>
                                <div class="form-group">
                                    <input class="form-control register-form" id="txtFirstName" type="text" runat="server" placeholder=" " ValidationGroup="exclusiveupdates">
                                    <asp:RequiredFieldValidator ID="reqtxtfirstname" ControlToValidate="txtFirstName" ValidationGroup="exclusiveupdates" CssClass="error-class" ErrorMessage="FirstName is Required" runat="server" ></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="label-form">
                                    Last Name <span class="astrik">*</span>
                                </label>
                                <div class="form-group">
                                    <input class="form-control register-form" id="txtLastName" type="text" runat="server"  placeholder="" ValidationGroup="exclusiveupdates">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtLastName" ValidationGroup="exclusiveupdates" CssClass="error-class" ErrorMessage="LastName is Required" runat="server" ></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="cl"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label class="label-form">
                                    Email Address <span class="astrik">*</span>
                                </label>
                                <div class="form-group">
                                    <input class="form-control register-form" id="txtEmail" type="email" runat="server" placeholder="" ValidationGroup="exclusiveupdates">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="exclusiveupdates" CssClass="error-class" ErrorMessage="Email is Required" runat="server" ></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="label-form">
                                    Phone Number
                                </label>
                                <div class="form-group">
                                    <input class="form-control register-form" id="txtPhone" type="tel" runat="server"  placeholder="" ValidationGroup="exclusiveupdates">
                                </div>
                            </div>
                            <div class="cl"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="label-form">
                                    City
                                </label>
                                <div class="form-group">
                                    <input class="form-control register-form" id="txtcity" type="text" runat="server"  autocomplete="off" placeholder="City" ValidationGroup="exclusiveupdates">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="label-form">
                                    State
                                </label>
                                <div class="form-group">
                                    <select style="padding: 4px 2px;" class="form-control" id="ddlstate" runat="server" ValidationGroup="exclusiveupdates">
                                        <option value="0">State</option>
                                        <option value="AB">Alberta</option>
                                        <option value="AL">Alabama</option>
                                         <option value="AK">Alaska</option>
                                         <option value="AZ">Arizona</option>
                                         <option value="AR">Arkansas</option>
                                         <option value="BC">British</option>
                                         <option value="CA">California</option>
                                         <option value="CO">Colorado</option>
                                         <option value="CT">Connecticut</option>
                                         <option value="DE">Delaware</option>
                                         <option value="DC">District Of Columbia</option>
                                         <option value="FL">Florida</option>
                                         <option value="GA">Georgia</option>
                                         <option value="HI">Hawaii</option>
                                         <option value="ID">Idaho</option>
                                         <option value="IL">Illinois</option>
                                         <option value="IN">Indiana</option>
                                         <option value="IA">Iowa</option>
                                         <option value="KS">Kansas</option>
                                         <option value="KY">Kentucky</option>
                                         <option value="LA">Louisiana</option>
                                         <option value="ME">Maine</option>
                                         <option value="MB">Manitoba</option>
                                         <option value="MD">Maryland</option>
                                         <option value="MA">Massachusetts</option>
                                         <option value="MI">Michigan</option>
                                         <option value="MN">Minnesota</option>
                                         <option value="MS">Mississippi</option>
                                         <option value="MO">Missouri</option>
                                         <option value="MT">Montana</option>
                                         <option value="NE">Nebraska</option>
                                         <option value="NV">Nevada</option>
                                         <option value="NB">New Brunswick</option>
                                         <option value="NH">New Hampshire</option>
                                         <option value="NJ">New Jersey</option>
                                        <option value="NM">New Mexico</option>
                                        <option value="NY">New York</option>
                                        <option value="NL">Newfoundland</option>
                                        <option value="NC">North Carolina</option>
                                        <option value="ND">North Dakota</option>
                                        <option value="NT">Northwest Territories</option>
                                        <option value="NS">Nova Scotia</option>
                                        <option value="NU">Nunavut</option>
                                        <option value="OH">Ohio</option>
                                        <option value="OK">Oklahoma</option>
                                        <option value="ON">Ontario</option>
                                        <option value="OR">Oregon</option>
                                        <option value="PA">Pennsylvania</option>
                                        <option value="PE">Prince Edward Island</option>
                                        <option value="QC">Quebec</option>
                                        <option value="RI">Rhode Island</option>
                                        <option value="SK">Saskatchewan</option>
                                        <option value="SC">South Carolina</option>
                                        <option value="SD">South Dakota</option>
                                        <option value="TN">Tennessee</option>
                                        <option value="TX">Texas</option>
                                        <option value="UT">Utah</option>
                                        <option value="VT">Vermont</option>
                                        <option value="VA">Virginia</option>
                                        <option value="WA">Washington</option>
                                        <option value="WV">West Virginia</option>
                                        <option value="WI">Wisconsin</option>
                                        <option value="WY">Wyoming</option>
                                        <option value="YT">Yukon</option>
                                    </select>
                                </div>
                            </div>
                            <div class="cl"></div>
                        </div>


                        <div class="row">
                            <div class="col-md-12">

                                <div id="errorMessage" >
                                    <h4 class="error-align" style="padding-left:15px; color:#dc554f;"></h4>
                                </div>
                                <div class="reg-form-align">
                                    <asp:Button class="btn reg-btn-align" ID="btnSubmit" runat="server" OnClick="btnsubmit_click" Text="Submit" ValidationGroup="exclusiveupdates" />

                                    <%--<button  class="btn reg-btn-align" type="submit" id="btnSubmit" runat="server" onclick="btnsubmit_click">Submit</button>--%>
                                </div>
                                <div id="divsignupexclusive" runat="server" style="display:none;">
                                  <h2 class="success-align">Thanks, you will get eclusive updates on your registered email address.</h2>
                               </div>

                            </div>
                        </div>

                    



                </div>

            </section> <!-- /.features-section -->
            <!-- Subscribe -->
            <section class="subscribe-section" id="section-3">

                <div class="container">

                    <div class="row">

                        <div class="col-md-8 register-section-col">

                            <!-- Title -->
                            <h2>Run a gym?</h2>

                            <p>We would love to have you in our network. Pedal is FREE to join and gives you access to users from across the country. Increasing your revenue without costing you a dime.
                                <br /><br />
                                <a href="#" class="more-questions contact-link">Have questions? We are here to help.</a>
                            </p>

                        </div> <!-- /.col-md-8 -->

                        <div class="col-md-4">

                            <p class="registration-buttons">
                                <a id="learn-more" class="btn btn-danger" href="gym.aspx">Learn More</a>
                            </p>

                            <p class="registration-buttons">
                                <a id="sign-in-front" class="btn btn-danger" href="/gym.html#/login">Sign In</a>
                            </p>

                        </div>  <!-- /.col-md-4 -->

                    </div> <!-- /.row -->

                </div> <!-- /.container -->

            </section> <!-- /.subscribe-section -->

            <section class="contact-section collapsing-section" id="section-4">

                <div class="container">

                    <div style="padding: 0 20px;" class="row">

                        <h1 class="get-in-touch-title">Get in touch</h1>

                       
                            <div class="input-field">

                                <div class="form-group">
                                    <input class="register-form form-control contact-form" id="email" type="email" runat="server"   placeholder="Your email address" ValidationGroup="gettouch">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="email" ValidationGroup="gettouch" CssClass="error-class" ErrorMessage="Email is Required" runat="server" ></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group">
                                    <input class="register-form form-control contact-form" id="name" type="text" runat="server"   placeholder="Your name" ValidationGroup="gettouch">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="name" ValidationGroup="gettouch" CssClass="error-class" ErrorMessage="Name is Required" runat="server" ></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <textarea rows="6" class="form-control contact-form" id="message" runat="server"    placeholder="What's up?" ValidationGroup="gettouch"></textarea>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="message" ValidationGroup="gettouch" CssClass="error-class" ErrorMessage="Message is Required" runat="server" ></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div>
                                <h2 class="error-align" style="color:#dc554f;"></h2>
                            </div>
                         <div id="errorMessage1" >
                                    <h4 class="error-align" style="padding-left:15px; color:#dc554f;"></h4>
                                </div>
                            <div class="get-in-touch-button">
                                <%--<button class="btn btn-danger btn-lg" type="submit">Send Message</button>--%>
                                <asp:Button class="btn btn-danger btn-lg" ID="btnsendmessage" runat="server" OnClick="btnGetTouch_click" Text="Send Message" ValidationGroup="gettouch"  />
                            </div>
                            <div  class="loading-logo" style="margin-left:60px; display:none;">
                                <svg version="1.0" xmlns="http://www.w3.org/2000/svg" width="50.000000pt" height="50.000000pt" viewBox="0 0 300.000000 300.000000" preserveAspectRatio="xMidYMid meet">
                                <g transform="translate(0.000000,300.000000) scale(0.100000,-0.100000)" stroke-width="5px">
                                <path fill="#CF3029" stroke="#CF3029" class="Animate-Draw" id="top-of-icon" d="M1245 2754 c-326 -46 -516 -233 -517 -509 0 -166 68 -289 292 -530
                                      199 -213 267 -307 307 -425 12 -36 28 -66 35 -68 7 -1 69 6 138 16 l125 19 -3
                                      33 c-5 48 -48 165 -88 236 -47 86 -152 218 -289 364 -193 206 -241 285 -232
                                      379 17 162 170 221 541 208 226 -8 332 -40 384 -117 21 -30 23 -44 19 -97 l-5
                                      -61 85 -52 c46 -28 99 -64 117 -81 19 -16 36 -29 39 -29 3 0 15 37 27 82 18
                                      66 21 102 18 183 -3 90 -7 109 -36 167 -60 123 -184 213 -357 259 -70 19 -110
                                      22 -325 25 -135 1 -258 0 -275 -2z" />
                                <path fill="#CF3029" stroke="#CF3029" class="Animate-Draw" id="bottom-of-icon" d="M1379 2239 c-123 -13 -279 -59 -279 -82 0 -14 41 -67 117 -153 l63
                                      -71 62 13 c255 54 528 -42 704 -248 60 -71 128 -209 149 -304 24 -104 16 -281
                                      -15 -374 -85 -250 -277 -428 -530 -491 -77 -19 -259 -17 -340 4 -201 53 -385
                                      204 -474 390 -115 239 -88 536 69 741 l36 48 -48 56 c-26 30 -65 78 -86 106
                                      -21 28 -43 51 -50 51 -20 0 -125 -147 -171 -240 -70 -141 -114 -336 -108 -475
                                      30 -660 651 -1119 1277 -944 184 51 335 143 468 284 291 310 356 752 167 1135
                                      -128 260 -374 460 -650 529 -60 14 -235 37 -270 34 -8 0 -49 -4 -91 -9z" />
                                </g>
                                </svg>
                            </div>
                       

                        <div id="divgettouch" runat="server" style="display:none;">
                            <h2 class="success-align">Thanks, we'll get back to you as soon as we can!</h2>
                        </div>


                    </div> <!-- /.row -->

                </div> <!-- /.container -->

            </section> <!-- /.contact-section -->

            <!-- Footer -->
            <footer class="footer-section" role="contentinfo">

                <div class="container">

                    <div class="row">

                        <div class="col-md-8 col-footer col-padding">

                            <!-- Footer 1 -->
                            <section>
                                <p style="margin:0;">Be sure to read our <a href="/index.html#/terms">Terms of Service</a>.</p>
                            </section>
                            <section>
                                <p style="margin:0;">Feel free to <a class="contact-link" href="#">get in touch</a> or email us at <a href="mailto:hello@pedal.com">hello@pedal.com</a>.</p>
                            </section>
 

                        </div> <!-- /.col-md-4 -->

                        <div class="col-md-4 col-footer">

                            <!-- Footer 1 -->
                            <section>
                                <p><strong>Pedal Holdings Inc.</strong> <br>San Francisco, CA</p>
                            </section>

                        </div> <!-- /.col-md-4 -->

                    </div> <!-- /.row -->

                </div> <!-- /.container -->

            </footer> <!-- /.footer-section -->

            <script src="http://code.jquery.com/jquery-1.10.2.js"></script>

            <script type="text/javascript">
                $(document).ready(function () {
                    $(".collapsing-section").hide();
                    $('.contact-link').click(function (e) {
                        e.stopPropagation();
                        $(".collapsing-section").slideToggle();
                        return false;
                    });
                });
            </script>
            <script src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
            <!-- Placed at the end of the document so the pages load faster -->
            <script src="images/jquery-2.1.0.min.js"></script>
            <script src="images/bootstrap.min.js"></script>
            <script src="images/application.js"></script>
            <!-- Visual Studio Browser Link -->
            <script type="application/json" id="__browserLink_initializationData">
                {"appName":"Chrome","requestId":"f7075d102e69480f8e2638f165dab181"}
            </script>
            
            <!-- End Browser Link -->
        </div>
        </form>
    </body>
</html>
