/*global module:false*/
module.exports = function (grunt) {

    // Project configuration.
    grunt.initConfig({
        // Metadata.
        pkg: grunt.file.readJSON('package.json'),
        less: {
            development: {
                files: {
                    "Content/site.css": "Content/site.less",
                    "lib/bootstrap/bootstrap.css": "bower_components/bootstrap/less/bootstrap.less"
                }
            }
        },
        cssmin: {
            target: {
                files: {
                    'Content/min/login.css': [
                        'lib/bootstrap/bootstrap.css',
                        'lib/angular-loading-bar/loading-bar.css',
                        'lib/font-awesome/font-awesome.css',
                        "Content/sb-admin-2.css",
                        "Content/angular-block-ui.min.css",
                    ],
                    'Content/min/app.css': [
                        'lib/bootstrap/bootstrap.css',
                        'lib/angular-loading-bar/loading-bar.css',
                        'lib/angular-ui-tree/angular-ui-tree.min.css',
                        'lib/font-awesome/font-awesome.css',
                        'lib/toastr/toastr.min.css',
                        "Content/sb-admin-2.css",
                        "Content/plugins/metisMenu/metisMenu.min.css",
                        "Content/plugins/timeline.css",
                        "Content/plugins/morris.css",
                        "Content/angular-block-ui.min.css",
                        'Content/site.css'
                    ]
                }
            }
        },
        uglify: {
            modernizr: {
                files: { 'scripts/min/modernizr.js': ['lib/modernizr/modernizr.js'] }
            },
            appLogin: {
                files: {
                    'Scripts/min/app-login.js': [
                            'lib/jquery/jquery.js',
                            'lib/angular/angular.js',
                            'lib/angular-local-storage/angular-local-storage.js',
                            'lib/angular-loading-bar/loading-bar.js',
                            'lib/bootstrap/bootstrap.js',
                            'app/app-login.js',
                            'app/services/authService.js',
                            'app/services/utilService.js',
                            'app/services/toastrService.js',
                            'app/directives/autoFillSync.js',
                            'app/controllers/loginController.js',
                            "scripts/angular-block-ui.min.js"
                    ]
                }
            },
            app: {
                files: {
                    'Scripts/min/app.js':
                    [
                        'lib/jquery/jquery.js',
                        'lib/bootstrap/bootstrap.js',
                        'lib/toastr/toastr.js',
                        'lib/moment/moment.js',
                        "scripts/plugins/metisMenu/metisMenu.min.js",
                        "scripts/sb-admin-2.js",

                    ]
                }
            },
            appAngular: {
                files: {
                    'Scripts/min/app-angular.js':
                    [
                        'lib/angular/angular.js',
                        'lib/angular-route/angular-route.js',
                        'lib/angular-local-storage/angular-local-storage.js',
                        'lib/angular-loading-bar/loading-bar.js',
                        'lib/angular-ui-tree/angular-ui-tree.min.js',
                        "scripts/angular-block-ui.min.js",
                        "scripts/angular-file-upload.min.js",
                        "app/app.js",
                        "app/filters/*.js",
                        "app/services/*.js",
                        "app/controllers/*.js",
                        "app/directives/*.js"
                    ]
                }
            }
        },
        watch: {
            intern: {
                files: ['app/*.js', 'app/*/*.js'],
                tasks: ['uglify:appAngular']
            },
            css: {
                files: ['Content/site.less'],
                tasks: ['less']
            }
        },
        bower: {
            install: {
                options: {
                    targetDir: 'lib',
                    layout: 'byType',
                    install: true,
                    verbose: false,
                    cleanTargetDir: false,
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
                      dest: 'content/fonts',
                      expand: true
                  },
                ],
            },
            bootstrap: {
                files: [
                  {
                      cwd: 'bower_components/bootstrap/fonts',
                      src: '**/*',
                      dest: 'content/fonts',
                      expand: true
                  },
                ],
            },
        }
    });

    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-copy');

    // Default task.
    grunt.registerTask('build', ['bower', 'less', 'uglify', 'cssmin', 'copy', 'watch']);
    grunt.registerTask('no-watch', ['bower', 'less', 'uglify', 'copy', 'cssmin']);

};
