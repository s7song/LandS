/*global module, require */
module.exports = function (grunt) {
    'use strict';

    var requireConfig = {
        baseUrl: 'app/',
        paths: {
            'text': '../Scripts/text',
            'jquery': '../Scripts/jquery-2.1.1',
            'durandal': '../Scripts/durandal',
            'plugins': '../Scripts/durandal/plugins',            
            'knockout': '../Scripts/knockout-3.2.0'
        }
    };
    var port = 3000;

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        port: port,
        clean: {
            build: ['build/*', 'app/**/*.js'],
            js: ['app/**/*.js']
        },
        copy: {
            css: {
                expand: true,
                src: 'sass/*.min.css',
                dest: 'build/',
            },
            requirejs: {
                expand: true,
                src: 'scripts/*.js',
                dest: 'build/',
            },
            images: {
                expand: true,
                src: 'images/**',
                dest: 'build/'
            },
            fonts: {
                expand: true,
                src: 'fonts/**',
                dest: 'build/'
            }
        },
        open: {
            dev: {
                path: 'http://localhost:<%= port %>',
            }
        },
        durandal: {
            main: {
                src: ['app/**/*.*', 'Scripts/durandal/**/*.js'],
                options: {
                    name: 'main',
                    baseUrl: requireConfig.baseUrl,
                    mainPath: 'app/main',
                    exclude: [],
                    paths: requireConfig.paths,
                    optimize: 'none',
                    out: 'build/app/main.js'
                }
            }
        },
        uglify: {
            options: {
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> \n' +
                    '* Copyright (c) <%= grunt.template.today("yyyy") %> YourName/YourCompany \n' +
                    '* Available via the MIT license.\n' +
                    '* see: http://opensource.org/licenses/MIT for blueprint.\n' +
                    '*/\n'
            },
            build: {
                src: 'build/app/main.js',
                dest: 'build/app/main-built.js'
            }
        },
        sass: {
            dev: {
                files: {                         // Dictionary of files
                    'sass/main.css': 'sass/main.scss',       // 'destination': 'source'                    
                }
            },
            build: {
                options: {
                    outputStyle: "compressed"
                },
                files: {
                    'sass/main.min.css': 'sass/main.scss',
                }
            }
        },
        watch: {
            options: {
                livereload: true
            },
            dev: {
                files: ['app/**/*.js', 'app/views/*.html', 'index.html'],
                tasks: ['sass'],

            },
            sass: {
                files: ['sass/*.scss'],
                tasks: ['sass'],
                options: {
                    livereload: false
                }
            },
            css: {
                files: ['sass/*.css'],
            },
            typescript: {
                options: {
                    livereload: false,
                },
                files: ['app/**/*.ts'],
                tasks: ["typescript"]
            },
        },
        express: {
            dev: {
                options: {
                    script: 'server.js',
                    node_env: "dev",
                }
            }
        },
        typescript: {
            build: {
                src: ['app/**/*.ts', 'Scripts/**/*.ts'],
                options: {
                    module: 'amd', //or commonjs
                    target: 'es5', //or es3
                    sourceMap: true,
                    declaration: false,
                    comments: true,
                }
            },
        },
        processhtml: {
            build: {
                files:
                   [
                       { 'build/index.html': 'index.html' },
                   ]
            }
        },
        'cache-busting': {
            requirejs: {
                replace: ['build/index.html'],
                replacement: 'main-built',
                file: 'build/app/main-built.js',
                cleanup: true
            },
            css: {
                replace: ['build/index.html'],
                replacement: 'main.min.css',
                file: 'build/sass/main.min.css',
                cleanup: true
            }
        },
    });

    // Loading plugin(s)
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-open');
    grunt.loadNpmTasks('grunt-durandal');
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-express-server');
    grunt.loadNpmTasks('grunt-typescript');
    grunt.loadNpmTasks('grunt-processhtml');
    grunt.loadNpmTasks('grunt-cache-busting');

    grunt.registerTask('default', ['sass:dev', "typescript", 'express', 'open', 'watch']);
    grunt.registerTask('build', ['clean:build', 'sass:build', "typescript", 'durandal', 'copy', 'processhtml', 'uglify', 'cache-busting']);
};
