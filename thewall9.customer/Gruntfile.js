/*global module:false*/
module.exports = function (grunt) {

    // Project configuration.
    grunt.initConfig({
        // Metadata.
        pkg: grunt.file.readJSON('package.json'),
        less: {
            vendors: {
                files: {
                    "lib/bootstrap/bootstrap.css": "bower_components/bootstrap/less/bootstrap.less"
                }
            },
            app: {
                files: {
                    "wwwroot/src/content/app.css": "wwwroot/src/content/app.less"
                }
            },
            login: {
                files: {
                    "wwwroot/src/content/login.css": "wwwroot/src/content/login.less"
                }
            }
        },
        cssmin: {
            vendors: {
                files: {
                    'wwwroot/build/css/app-vendors.min.css': [
                        'lib/bootstrap/bootstrap.css',
                        'bower_components/font-awesome/css/font-awesome.min.css',
                        'lib/angular-block-ui/angular-block-ui.css',
                        'lib/toastr/toastr.css',
                        'wwwroot/src/content/sb-admin.css',
                        //ANGULAR ui.tree
                        'lib/angular-ui-tree/angular-ui-tree.min.css',
                    ] 
                }
            },
            app: {
                files: {
                    'wwwroot/build/css/app.min.css': ['wwwroot/src/content/app.css'],
                }
            },
            loginVendors: {
                files: {
                    'wwwroot/build/css/login-vendors.min.css': [
                        'lib/bootstrap/bootstrap.css',
                        'bower_components/font-awesome/css/font-awesome.min.css',
                        'lib/angular-block-ui/angular-block-ui.css',
                        'lib/toastr/toastr.css',
                        'wwwroot/src/content/sb-admin.css'
                    ]
                }
            },
            login: {
                files: {
                    'wwwroot/build/css/login.min.css': ['wwwroot/src/content/login.css'],
                }
            },
            //target: {
            //    files: {
            //        'Content/min/login.css': [
            //            'lib/bootstrap/bootstrap.css',
            //            'lib/angular-loading-bar/loading-bar.css',
            //            'lib/font-awesome/font-awesome.css',
            //            "Content/sb-admin-2.css",
            //            "lib/angular-block-ui/angular-block-ui.css",
            //        ],
            //        'Content/min/app.css': [
            //            'lib/bootstrap/bootstrap.css',
            //            'lib/angular-loading-bar/loading-bar.css',
            //            'lib/angular-ui-tree/angular-ui-tree.min.css',
            //            'bower_components/font-awesome/css/font-awesome.min.css',
            //            'lib/toastr/toastr.css',
            //            "Content/sb-admin-2.css",
            //            "Content/plugins/metisMenu/metisMenu.min.css",
            //            "Content/plugins/timeline.css",
            //            "Content/plugins/morris.css",
            //            "lib/angular-block-ui/angular-block-ui.css",
            //            'Content/site.css'
            //        ]
            //    }
            //}
        },
        uglify: {
            vendors: {
                files: {
                    'wwwroot/build/js/app-vendors.min.js':
                    [
                        'lib/jquery/jquery.js',
                        'lib/bootstrap/bootstrap.js',
                        'lib/toastr/toastr.js',
                        // ANGULAR
                        'lib/angular/angular.js',
                        'lib/angular-route/angular-route.js',
                        'lib/angular-local-storage/angular-local-storage.js',
                        'lib/angular-block-ui/angular-block-ui.js',
                        'lib/angular-bootstrap/ui-bootstrap-tpls.js',
                        
                        //ANGULAR ui.tree
                        'lib/angular-ui-tree/angular-ui-tree.js',
                        //FILE UPLOAD
                        'bower_components/ng-file-upload/ng-file-upload-shim.min.js',
                        'bower_components/ng-file-upload/ng-file-upload.min.js',
                        //TINYMCE
                        //'lib/tinymce-dist/tinymce.min.js',
                        'lib/angular-ui-tinymce/tinymce.js',
                        //DATE PICKER
                        "lib/moment/moment.js",
                        "lib/bootstrap-daterangepicker/daterangepicker.js",
                    ]
                }
            },
            app: {
                files: {
                    'wwwroot/build/js/app.min.js':
                    [
                        "wwwroot/src/js/app.js",
                        "wwwroot/src/app/app.js",
                        "wwwroot/src/app/filters/*.js",
                        "wwwroot/src/app/services/*.js",
                        "wwwroot/src/app/controllers/*.js",
                        "wwwroot/src/app/directives/*.js"
                    ]
                }
            },
            loginVendors: {
                files: {
                    'wwwroot/build/js/login-vendors.min.js':
                    [
                        'lib/jquery/jquery.js',
                        'lib/bootstrap/bootstrap.js',
                        'lib/toastr/toastr.js',
                        'lib/angular/angular.js',
                        'lib/angular-local-storage/angular-local-storage.js',
                        'lib/angular-block-ui/angular-block-ui.js'
                    ]
                }
            },
            login: {
                files: {
                    'wwwroot/build/js/login.min.js':
                    [
                        "wwwroot/src/app/app-login.js",
                        "wwwroot/src/app/services/*.js",
                        "wwwroot/src/app/controllers/loginController.js",
                        "wwwroot/src/app/directives/autoFillSync.js"
                    ]
                }
            }
        },
        watch: {
            app: {
                files: ['wwwroot/src/js/app.js', 'wwwroot/src/app/app.js', 'wwwroot/src/app/*/*.js'],
                tasks: ['uglify:app']
            },
            css: {
                files: ['wwwroot/src/content/app.less', 'wwwroot/src/content/mixins.less', 'wwwroot/src/content/variables.less'],
                tasks: ['less:app', 'cssmin:app']
            },
            login: {
                files: ['wwwroot/src/app/login.js', 'wwwroot/src/app/*/*.js'],
                tasks: ['uglify:login']
            },
            loginCss: {
                files: ['wwwroot/src/content/login.less', 'wwwroot/src/content/mixins.less', 'wwwroot/src/content/variables.less'],
                tasks: ['less:login', 'cssmin:login']
            }
        },
        bower: {
            install: {
                options: {
                    targetDir: 'lib',
                    layout: 'byType',
                    install: true,
                    verbose: false,
                    cleanTargetDir: true,
                    cleanBowerDir: false,
                    bowerOptions: {}
                }
            }
        },
        copy: {
            fontAwesome: {
                files: [
                  {
                      cwd: 'bower_components/font-awesome/fonts',
                      src: '**/*',
                      dest: 'wwwroot/build/fonts',
                      expand: true
                  }
                ]
            },
            bootstrap: {
                files: [
                  {
                      cwd: 'bower_components/bootstrap/fonts',
                      src: '**/*',
                      dest: 'wwwroot/build/fonts',
                      expand: true,

                  }
                ]
            },
            tinymce: {
                files: [{
                      cwd: 'lib/tinymce-dist/',
                      src: 'tinymce.min.js',
                      dest: 'wwwroot/build/js/',
                      expand: true
                  },{
                      cwd: 'bower_components/tinymce-dist/skins',
                      src: '**/*',
                      dest: 'wwwroot/build/js/skins',
                      expand: true
                  },{
                      cwd: 'bower_components/tinymce-dist/themes',
                      src: '**/*',
                      dest: 'wwwroot/build/js/themes',
                      expand: true
                  }, {
                      cwd: 'bower_components/tinymce-dist/plugins',
                      src: '**/*',
                      dest: 'wwwroot/build/js/plugins',
                      expand: true
                  }]
            }
        }
    });

    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-copy');

    // Default task.
    grunt.registerTask('build', ['bower', 'less', 'uglify', 'cssmin', 'watch']);
    grunt.registerTask('no-watch', ['bower', 'less', 'uglify', 'copy', 'cssmin']);

};
